using System;
using System.Collections.Generic;
using System.Reflection;
using SqlUserTypeGenerator.ColumnTextGenerators;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator
{
	internal class ColumnTextGeneratorFactory
	{

		private static readonly Dictionary<Type, GeneratorCreateFunc> Generators = new Dictionary<Type, GeneratorCreateFunc>()
		{
			//todo - get rid of "new" operator, use currying
			{ typeof(Int64), (info) =>  new ColumnTextGenerator("bigint", info) },
			{ typeof(string), (info) =>  new NvarcharColumnGenerator("nvarchar", info) },
			{ typeof(decimal), (info) =>  new DecimalColumnGenerator("numeric", info) },
			{ typeof(Boolean), (info) =>  new ColumnTextGenerator("bit", info) } ,
			{ typeof(DateTime), (info) =>  new ColumnTextGenerator("datetime", info) } ,
			{ typeof(Double), (info) =>  new ColumnTextGenerator("float", info) } ,
			{ typeof(Int32), (info) =>  new ColumnTextGenerator("int", info) } ,
			{ typeof(Guid), (info) =>  new ColumnTextGenerator("uniqueidentifier", info) } ,
			{ typeof(Byte[]), (info) =>  new ColumnTextGenerator("varbinary", info) } ,
			{ typeof(Byte), (info) =>  new ColumnTextGenerator("tinyint", info) } ,			
		};

		private delegate IColumnTextGenerator GeneratorCreateFunc(PropertyInfo pi);

		public static IColumnTextGenerator CreateGenerator(PropertyInfo pi)
		{
			var propertyBaseType = TypeHelper.ExtractNonNullableType(pi);

			var sourceType = propertyBaseType;

			if (propertyBaseType.IsEnum)
			{
				sourceType = typeof(Int32); //Enum.GetUnderlyingType(propertyBaseType);
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
