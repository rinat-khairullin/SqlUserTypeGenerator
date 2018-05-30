using System;
using System.Collections.Generic;
using System.Reflection;
using SqlUserTypeGenerator.ColumnTextGenerators;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator
{
	internal class ColumnTextGeneratorFactory
	{
		private delegate IColumnTextGenerator GeneratorCreateFunc(PropertyInfo pi, GenerateUserTypeSettings settings);

		private static readonly Dictionary<Type, GeneratorCreateFunc> Generators = new Dictionary<Type, GeneratorCreateFunc>()
		{
			//todo - get rid of "new" operator, use currying
			{typeof(long), (info, settings) => new ColumnTextGenerator("bigint", info)},
			{typeof(string), (info, settings) => new NvarcharColumnGenerator("nvarchar", info)},
			{typeof(decimal), (info, settings) => new DecimalColumnGenerator("numeric", info)},
			{typeof(bool), (info, settings) => new ColumnTextGenerator("bit", info)},
			{typeof(DateTime), (info, settings) => new ColumnTextGenerator(settings.UseSqlDateTime2 ? "datetime2" : "datetime", info)},
			{typeof(double), (info, settings) => new ColumnTextGenerator("float", info)},
			{typeof(int), (info, settings) => new ColumnTextGenerator("int", info)},
			{typeof(Guid), (info, settings) => new ColumnTextGenerator("uniqueidentifier", info)},
			{typeof(byte[]), (info, settings) => new ColumnTextGenerator("varbinary", info)},
			{typeof(byte), (info, settings) => new ColumnTextGenerator("tinyint", info)},
		};

		public static IColumnTextGenerator CreateGenerator(PropertyInfo pi, GenerateUserTypeSettings settings)
		{
			var propertyBaseType = TypeHelper.ExtractNonNullableType(pi);

			var sourceType = propertyBaseType;

			if (propertyBaseType.IsEnum)
			{
				sourceType = typeof(int); //!_!Enum.GetUnderlyingType(propertyBaseType);
			}

			if (Generators.ContainsKey(sourceType))
			{
				return Generators[sourceType](pi, settings);
			}
			else
			{
				return null;
			}
		}

	}
}
