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

		private delegate ColumnTextGenerator GeneratorCreateFunc(PropertyInfo pi);

		public static ColumnTextGenerator CreateGenerator(PropertyInfo pi)
		{
			var propertyBaseType = TypeHelper.ExtractNonNullableType(pi);
			if (Generators.ContainsKey(propertyBaseType))
			{
				return Generators[propertyBaseType](pi);
			}
			else
			{
				//todo - log error
				throw new NotImplementedException($"generator for {pi.PropertyType.FullName} {pi.Name} not implemented");
			}
		}

	}
}
