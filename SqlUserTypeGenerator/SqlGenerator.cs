using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator
{
    public class SqlGenerator
    {


		internal static SqlUserTypeDefinition GenerateUserType(Type type, CustomAttributeData sqlUserTypeAttributeData)
        {
            var cols = type.GetProperties();

            List<string> sqlColumns = new List<string>();
            foreach (var c in cols)
            {
                sqlColumns.Add(CreateSqlColumnString(c));
            }
				        
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
            return $"{gen.GetColumnName()} {gen.GetColumnType()} {gen.GetColumnNullability()}";
        }

    }
}