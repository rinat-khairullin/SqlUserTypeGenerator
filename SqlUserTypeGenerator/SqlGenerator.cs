using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator
{
    public class SqlGenerator
    {


		internal static SqlUserTypeDefinition GenerateUserType(Type type, CustomAttributeData sqlUserTypeAttributeData)
        {
            var cols = type.GetProperties();

            IList<string> sqlColumns = cols
				.Select(CreateSqlColumnString)
				.Where(s => !string.IsNullOrEmpty(s))
				.ToList()
				;
				        
	        var typeNameFromAttr = CustomAttributesHelper.GetTypeName(sqlUserTypeAttributeData);	        			

			return new SqlUserTypeDefinition()
            {
                TypeName = typeNameFromAttr ?? type.Name,
				Columns = sqlColumns
            };
        }

        internal static string CreateSqlColumnString(PropertyInfo property)
        {
                        
	        var gen = ColumnTextGeneratorFactory.CreateGenerator(property);
	        return gen != null ? $"{gen.GetColumnName()} {gen.GetColumnType()} {gen.GetColumnNullability()}" : string.Empty;            
        }

    }
}