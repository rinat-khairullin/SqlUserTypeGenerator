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
			//todo - get rid of "new" operator, use currying
			{typeof(long), (info) => new ColumnTextGenerator("bigint", info)},
			{typeof(string), (info) => new NvarcharColumnGenerator("nvarchar", info)},
			{typeof(decimal), (info) => new DecimalColumnGenerator("numeric", info)},
			{typeof(bool), (info) => new ColumnTextGenerator("bit", info)},
			{typeof(DateTime), (info) => new DateColumnGenerator(info)},
			{typeof(double), (info) => new ColumnTextGenerator("float", info)},
			{typeof(int), (info) => new ColumnTextGenerator("int", info)},
			{typeof(Guid), (info) => new ColumnTextGenerator("uniqueidentifier", info)},
			{typeof(byte[]), (info) => new ColumnTextGenerator("varbinary", info)},
			{typeof(byte), (info) => new ColumnTextGenerator("tinyint", info)},
		};

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
