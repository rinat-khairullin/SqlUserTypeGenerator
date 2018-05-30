using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator
{
	public class SqlGenerator
	{
		internal static SqlUserTypeDefinition GenerateUserType(Type type, CustomAttributeData sqlUserTypeAttributeData, GenerateUserTypeSettings settings)
		{
			var cols = type.GetProperties();

			IList<string> sqlColumns = cols
				.Select(i => CreateSqlColumnString(i, settings))
				.Where(s => !string.IsNullOrEmpty(s))
				.ToList();

			var typeNameFromAttr = CustomAttributesHelper.GetTypeName(sqlUserTypeAttributeData);

			return new SqlUserTypeDefinition()
			{
				TypeName = typeNameFromAttr ?? type.Name,
				Columns = sqlColumns
			};
		}

		internal static string CreateSqlColumnString(PropertyInfo property, GenerateUserTypeSettings settings)
		{
			var gen = ColumnTextGeneratorFactory.CreateGenerator(property, settings);
			return gen != null ? $"{gen.GetColumnName()} {gen.GetColumnType()} {gen.GetColumnNullability()}" : string.Empty;
		}
	}

	public class GenerateUserTypeSettings
	{
		public bool UseSqlDateTime2 { get; set; } = true;
	}
}
