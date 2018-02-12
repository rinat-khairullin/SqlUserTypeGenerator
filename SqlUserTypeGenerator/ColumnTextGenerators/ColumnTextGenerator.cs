using System.Reflection;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class ColumnTextGenerator : IColumnTextGenerator
	{
		private readonly string _sqlTypeName;
		private readonly PropertyInfo _propertyInfo;

		public ColumnTextGenerator(string sqlTypeName, PropertyInfo propertyInfo)
		{
			_sqlTypeName = sqlTypeName;
			_propertyInfo = propertyInfo;
		}

		public string GetColumnName()
		{
			return ColumnTextUtils.GetColumnName(_propertyInfo);
		}

		public virtual string GetColumnType()
		{
			var columnLengthString = string.Empty;			
			return ColumnTextUtils.GetColumnTypeString(_sqlTypeName, columnLengthString);
		}

		public string GetColumnNullability()
		{
			return ColumnTextUtils.GetColumnNullability(_propertyInfo.PropertyType);			
		}		
	}
}