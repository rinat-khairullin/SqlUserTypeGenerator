using System.Globalization;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class NvarcharColumnGenerator : IColumnTextGenerator
	{
		private readonly string _sqlTypeName;
		private readonly PropertyInfo _propertyInfo;

		public NvarcharColumnGenerator(string sqlTypeName, PropertyInfo propertyInfo)
		{
			_sqlTypeName = sqlTypeName;
			_propertyInfo = propertyInfo;
		}

		public string GetColumnName()
		{
			return ColumnTextUtils.GetColumnName(_propertyInfo);
		}

		public string GetColumnType()
		{
			var columnLengthString = "50";			
			var columnLengthFromAttr = CustomAttributesHelper.GetSqlUserTypeColumnLength(_propertyInfo);
			if (columnLengthFromAttr.HasValue)
			{
				columnLengthString = columnLengthFromAttr.Value == SqlColumnAttribute.MaxLength ? "max" : columnLengthFromAttr.Value.ToString(CultureInfo.InvariantCulture);
			}
			return ColumnTextUtils.GetColumnTypeString(_sqlTypeName, columnLengthString);			
		}

		public string GetColumnNullability()
		{
			var attr = CustomAttributesHelper.GetSqlUserTypeColumnNullable(_propertyInfo);
			if (attr.HasValue)
			{
				return ColumnTextUtils.GetColumnNullability(attr.Value);
			}
			return ColumnTextUtils.GetColumnNullability(_propertyInfo.PropertyType);
		}
	}
}