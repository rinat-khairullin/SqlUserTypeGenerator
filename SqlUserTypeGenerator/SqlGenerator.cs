using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace SqlUserTypeGenerator
{
    public class SqlGenerator
    {
        private static readonly Dictionary<Type, string> SupportedClrTypes = new Dictionary<Type, string>()
        {
            { typeof(Int64), "bigint" } ,
            { typeof(string), "nvarchar" } ,
            { typeof(Boolean), "bit" } ,
            { typeof(DateTime), "datetime" } ,
            { typeof(Double), "float" } ,
            { typeof(Int32), "int" } ,
            { typeof(Decimal), "numeric" } ,
            { typeof(Guid), "uniqueidentifier" } ,
            { typeof(Byte[]), "varbinary" } ,
            { typeof(Byte), "bit" } ,

        };        

        public static Dictionary<Type, string> DefaultTypesLengths  = new Dictionary<Type, string>()
        {
            { typeof(string), "50" },
            { typeof(decimal), "18"}
        };

        internal static SqlUserTypeDefinition GenerateUserType(Type type)
        {
            var cols = type.GetProperties();

            List<string> sqlColumns = new List<string>();
            foreach (var c in cols)
            {
                sqlColumns.Add(CreateSqlColumnString(c));

            }
            return new SqlUserTypeDefinition()
            {
                TypeName = type.GetCustomAttribute<SqlUserTypeAttribute>().TypeName,
                Columns = sqlColumns
            };
        }

        private static string CreateSqlColumnString(PropertyInfo property)
        {

            var sqlType = GetSqlType(property);
            if (string.IsNullOrEmpty(sqlType))
            {
                return $"/* warning: type {property.PropertyType} is not supported */";
            }

            return $"{GetColumnName(property)} {sqlType} {GetColumnNullability(property)}";
        }

        private static string GetColumnNullability(PropertyInfo property)
        {
            var isNullable = IsNullableType(property.PropertyType);
            return isNullable ? "null" : "not null";
        }

        private static bool IsNullableType(Type propType)
        {
            return GetNonNullableType(propType) != null;
        }

        private static string GetColumnName(PropertyInfo property)
        {
            return property.Name;
        }

        private static string GetSqlType(PropertyInfo property)
        {
            var notNullableType = ExtractNonNullableType(property);
            var typeName = string.Empty;

            if (SupportedClrTypes.ContainsKey(notNullableType))
            {
                typeName = SupportedClrTypes[notNullableType];
            }

            var columnLengthString = string.Empty;

            if (DefaultTypesLengths.ContainsKey(property.PropertyType))
            {
                var columnLength = DefaultTypesLengths[property.PropertyType];
                var columnProps = property.GetCustomAttribute<SqlUserTypeColumnPropertiesAttribute>();
                if (columnProps != null)
                {
                    columnLength = columnProps.Length == SqlUserTypeColumnPropertiesAttribute.MaxLength ? "max" : columnProps.Length.ToString(CultureInfo.InvariantCulture);
                }
                columnLengthString = $"({columnLength})";
            }


            return $"{typeName}" + (!string.IsNullOrEmpty(columnLengthString) ? columnLengthString : "");

        }

        private static Type GetNonNullableType(Type propertyType)
        {
            return Nullable.GetUnderlyingType(propertyType);
        }

        private static Type ExtractNonNullableType(PropertyInfo pi)
        {
            return GetNonNullableType(pi.PropertyType) ?? pi.PropertyType;
        }
    }
}