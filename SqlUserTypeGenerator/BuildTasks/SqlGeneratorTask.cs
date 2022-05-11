using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Build.Framework;
using SqlUserTypeGenerator.Helpers;

// ReSharper disable UnusedType.Global

namespace SqlUserTypeGenerator
{
	/// <summary> Main msbuild sql generator task </summary>
	public class SqlGeneratorTask : ITask
	{
		private readonly string _newLine = Environment.NewLine;

		/// <summary> Assembly to inspect for classes of sql-user types </summary>
		public string SourceAssemblyPath { get; set; }

		/// <summary> Absolute path to generated files </summary>
		public string DestinationFolder { get; set; }

		/// <summary> Text block that will be inserted before sql-generated type </summary>
		public string EncodedTypePreCreateCode { get; set; }

		/// <summary> Text block that will be inserted after sql-generated type </summary>
		public string EncodedTypePostCreateCode { get; set; }

		/// <summary> List of ignored files, delimited by ; or newline </summary>
		public string EncodedIgnore { get; set; }

		/// <inheritdoc />
		public IBuildEngine BuildEngine { get; set; }

		/// <inheritdoc />
		public ITaskHost HostObject { get; set; }

		/// <inheritdoc />
		public bool Execute()
		{
			try
			{
				if (!Directory.Exists(DestinationFolder))
				{
					Directory.CreateDirectory(DestinationFolder);
				}

				AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;

				LoadDependentAssemblies(SourceAssemblyPath, GetIgnoreFiles());

				//load target assembly
				var assembly = Assembly.ReflectionOnlyLoadFrom(SourceAssemblyPath);

				var types = GetTypesWithSqlTypeAttribute(assembly);

				var headerText = GetHeaderText();

				foreach (var type in types)
				{
					var generatedType = SqlGenerator.GenerateUserType(type.UserType, type.SqlUserTypeAttributeData);

					var generatedSql = BuildSqlText(generatedType);

					var targetFile = Path.ChangeExtension(Path.Combine(DestinationFolder, GetSafeFilename(generatedType.TypeName)), "sql");

					File.WriteAllText(targetFile, headerText + generatedSql, Encoding.UTF8);
				}

				return true;
			}
			catch (ReflectionTypeLoadException exc)
			{
				var msg = FaultHelper.GetMessage(exc);
				BuildEngine.LogErrorEvent(FaultHelper.CreateErrorEvent(FaultHelper.AssemblyLoadError, SourceAssemblyPath, msg));
			}
			catch (Exception exc)
			{
				var msg = FaultHelper.GetMessageCore(exc);
				BuildEngine.LogErrorEvent(FaultHelper.CreateErrorEvent(FaultHelper.GeneralError, SourceAssemblyPath, msg));
			}

			return false;
		}

		private HashSet<string> GetIgnoreFiles()
		{
			var result = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

			var ignoredFiles = StringHelper.DecodeArgument(EncodedIgnore)
				?.Split(new[] { ';', (char)10, (char)13 }, StringSplitOptions.RemoveEmptyEntries)
				.Where(x => !string.IsNullOrWhiteSpace(x))
				.Select(x => x.Trim());

			if (ignoredFiles == null)
			{
				return result;
			}

			foreach (var file in ignoredFiles)
			{
				result.Add(file);
			}

			return result;
		}

		private List<UserTypeWithSqlUserTypeAttribute> GetTypesWithSqlTypeAttribute(Assembly assembly)
		{
			return assembly.GetTypes()
				.Select(t => new UserTypeWithSqlUserTypeAttribute
				{
					UserType = t,
					SqlUserTypeAttributeData = CustomAttributesHelper.GetSqlUserTypeAttributeData(t)
				})
				.Where(ut => ut.SqlUserTypeAttributeData != null)
				.ToList();
		}

		private string BuildSqlText(SqlUserTypeDefinition generatedType)
		{
			var typeNameReplaceString = "$typename$";
			string typePreCreateCode = StringHelper.DecodeArgument(EncodedTypePreCreateCode)?.Replace(typeNameReplaceString, generatedType.TypeName);
			string typePostCreateCode = StringHelper.DecodeArgument(EncodedTypePostCreateCode)?.Replace(typeNameReplaceString, generatedType.TypeName);

			return string.Empty
				+ (!string.IsNullOrEmpty(typePreCreateCode) ? $"{typePreCreateCode}{_newLine}" : string.Empty)
				+ $"create type [{generatedType.TypeName}] as table ({_newLine}"
				+ string.Join($",{_newLine}", generatedType.Columns.Select(c => "\t" + c))
				+ $"{_newLine}){_newLine}go"
				+ (!string.IsNullOrEmpty(typePostCreateCode) ? $"{_newLine}{typePostCreateCode}" : string.Empty)
				;
		}

		private void LoadDependentAssemblies(string sourceAssemblyPath, HashSet<string> ignoredFiles)
		{
			//load all assemblies in project output folder to reflection-only context
			var directoryName = Path.GetDirectoryName(sourceAssemblyPath);

			var files = Directory.GetFiles(directoryName)
				.Where(f => StringHelper.IsEqualStrings(Path.GetExtension(f), ".dll"))
				.Where(x => !ignoredFiles.Contains(Path.GetFileName(x)));

			foreach (var file in files)
			{
				try
				{
					Assembly.ReflectionOnlyLoadFrom(file);
				}
				// ignore native dll loading errors
				catch (BadImageFormatException exc)
				{
					var msg = FaultHelper.GetMessageCore(exc);
					BuildEngine.LogErrorEvent(FaultHelper.CreateErrorEvent(FaultHelper.AssemblyLoadError, file, msg));
				}
			}
		}

		internal class UserTypeWithSqlUserTypeAttribute
		{
			public Type UserType { get; set; }
			public CustomAttributeData SqlUserTypeAttributeData { get; set; }
		}

		private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
		{
			var missedAssemblyFullName = args.Name;
			var assembly = Assembly.ReflectionOnlyLoad(missedAssemblyFullName);
			return assembly;
		}

		private string GetHeaderText()
		{
			var assembly = typeof(SqlGeneratorTask).Assembly;
			var an = assembly.GetName().Name;
			var av = assembly.GetName().Version.ToString();
			return $"--autogenerated by {an} v{av}{_newLine + _newLine}";
		}

		private string GetSafeFilename(string filename)
		{
			return string.Join("", filename.Split(Path.GetInvalidFileNameChars()));
		}
	}
}
