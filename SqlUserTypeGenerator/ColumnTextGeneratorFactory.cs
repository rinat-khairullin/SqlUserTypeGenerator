using System;
using System.Collections.Generic;
using System.Reflection;
using SqlUserTypeGenerator.ColumnTextGenerators;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator
{
	internal class ColumnTextGeneratorFactory
	{
		private delegate IColumnTextGenerator GeneratorCreateFunc(PropertyInfo pi);

		private static readonly Dictionary<Type, GeneratorCreateFunc> Generators = new Dictionary<Type, GeneratorCreateFunc>()
		{
			{ typeof(long), CreateColumnTextGenerator("bigint") },
			{ typeof(string), CreateNvarcharColumnGenerator("nvarchar") },
			{ typeof(decimal), CreateDecimalColumnGenerator() },
			{ typeof(bool), CreateColumnTextGenerator("bit") },
			{ typeof(DateTime), CreateDateColumnGenerator() },
			{ typeof(double), CreateColumnTextGenerator("float") },
			{ typeof(int), CreateColumnTextGenerator("int") },
			{ typeof(Guid), CreateColumnTextGenerator("uniqueidentifier") },
			{ typeof(byte[]), CreateNvarcharColumnGenerator("varbinary") },
			{ typeof(byte), CreateColumnTextGenerator("tinyint") },
		};

		private static GeneratorCreateFunc CreateColumnTextGenerator(string typeName)
		{
			return (propInfo) => new ColumnTextGenerator(typeName, propInfo);
		}

		private static GeneratorCreateFunc CreateNvarcharColumnGenerator(string typeName)
		{
			return (propInfo) => new NvarcharColumnGenerator(typeName, propInfo);
		}

		private static GeneratorCreateFunc CreateDecimalColumnGenerator()
		{
			return (propInfo) => new DecimalColumnGenerator("numeric", propInfo);
		}

		private static GeneratorCreateFunc CreateDateColumnGenerator()
		{
			return (propInfo) => new DateColumnGenerator(propInfo);
		}

		public static IColumnTextGenerator CreateGenerator(PropertyInfo pi)
		{
			var propertyBaseType = TypeHelper.ExtractNonNullableType(pi);

			var sourceType = propertyBaseType;

			if (propertyBaseType.IsEnum)
			{
				sourceType = typeof(int); //!_!Enum.GetUnderlyingType(propertyBaseType);
			}

			if (Generators.ContainsKey(sourceType))
			{
				return Generators[sourceType](pi);
			}
			else
			{
				return null;
			}
		}
	}
}
