using System;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class ColumnTextUtils
	{
		private static string _nullStr = "null";
		private static string notNullStr = "not null";

		public static string GetColumnTypeString(string typeName, string columnLengthString)
		{
			return $"{typeName}" + (!string.IsNullOrEmpty(columnLengthString) ? $"({columnLengthString})" : string.Empty);
		}

		public static string GetColumnNullability(Type propType)
		{
			var isNullable = TypeHelper.IsNullableType(propType);
			return isNullable ? _nullStr : notNullStr;
		}

		public static string GetColumnNullability(bool isNull)
		{			
			return isNull ? _nullStr : notNullStr;
		}


		public static string GetColumnName(PropertyInfo propertyInfo)
		{
			return propertyInfo.Name;
		}
	}
}