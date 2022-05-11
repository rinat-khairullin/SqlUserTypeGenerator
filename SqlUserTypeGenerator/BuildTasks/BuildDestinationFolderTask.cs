using Microsoft.Build.Framework;

// ReSharper disable UnusedType.Global

namespace SqlUserTypeGenerator.BuildTasks
{
	/// <summary> Task for simplifying generation of output folder where sql-user types will be stored </summary>
	public class BuildDestinationFolderTask : ITask
	{
		/// <inheritdoc />
		public IBuildEngine BuildEngine { get; set; }

		/// <inheritdoc />
		public ITaskHost HostObject { get; set; }

		/// <summary> Output path </summary>
		[Output]
		public string Path { get; set; }

		/// <summary> Custom path to output folder </summary>
		public string Value { get; set; }

		/// <summary> Project directory location </summary>
		public string ProjectDir { get; set; }

		/// <inheritdoc />
		public bool Execute()
		{
			Path = System.IO.Path.Combine(ProjectDir, "GeneratedSqlTypes");
			if (!string.IsNullOrEmpty(Value))
			{
				var trimmed = Value.TrimEnd('\\');
				Path = System.IO.Path.GetFullPath(trimmed);
			}

			return true;
		}
	}
}
