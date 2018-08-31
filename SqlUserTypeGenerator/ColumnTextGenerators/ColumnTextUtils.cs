using System;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class ColumnTextUtils
	{
		private const string NullStr = "null";
		private const string NotNullStr = "not null";

		public static string GetColumnTypeString(string typeName, string columnLengthString)
		{
			return $"{typeName}" + (!string.IsNullOrEmpty(columnLengthString) ? $"({columnLengthString})" : string.Empty);
		}

		public static string GetColumnNullability(Type propType)
		{
			var isNullable = TypeHelper.IsNullableType(propType);
			return isNullable ? NullStr : NotNullStr;
		}
		public static string GetColumnNullability(bool isNull)
		{			
			return isNull ? NullStr : NotNullStr;
		}

		public static string GetColumnName(PropertyInfo propertyInfo)
		{
			return CustomAttributesHelper.GetSqlDateColumnName(propertyInfo) ?? propertyInfo.Name;
		}
	}
}
