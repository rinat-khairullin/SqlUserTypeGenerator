using System;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class ColumnTextUtils
	{
		public static string GetColumnTypeString(string typeName, string columnLengthString)
		{
			return $"{typeName}" + (!string.IsNullOrEmpty(columnLengthString) ? $"({columnLengthString})" : string.Empty);
		}

		public static string GetColumnNullability(Type propType)
		{
			var isNullable = TypeHelper.IsNullableType(propType);
			return isNullable ? "null" : "not null";
		}

		public static string GetColumnName(PropertyInfo propertyInfo)
		{
			return propertyInfo.Name;
		}
	}
}