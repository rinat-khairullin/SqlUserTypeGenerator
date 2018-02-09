using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class ColumnTextGenerator
	{
		protected readonly string SqlTypeName;
		protected readonly PropertyInfo PropertyInfo;

		public ColumnTextGenerator(string sqlTypeName, PropertyInfo propertyInfo)
		{
			SqlTypeName = sqlTypeName;
			PropertyInfo = propertyInfo;
		}

		public string GetColumnName()
		{
			return PropertyInfo.Name;
		}

		public virtual string GetColumnType()
		{
			var columnLengthString = string.Empty;			
			return GetColumnTypeString(SqlTypeName, columnLengthString);
		}

		protected string GetColumnTypeString(string typeName, string columnLengthString)
		{
			return $"{typeName}" + (!string.IsNullOrEmpty(columnLengthString) ? $"({columnLengthString})" : string.Empty);
		}

		public string GetColumnNullability()
		{
			var isNullable = TypeHelper.IsNullableType(PropertyInfo.PropertyType);
			return isNullable ? "null" : "not null";
		}
	}
}