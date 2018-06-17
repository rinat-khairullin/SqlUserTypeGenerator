using System;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class DateColumnGenerator : IColumnTextGenerator
	{
		private readonly PropertyInfo _propertyInfo;

		public DateColumnGenerator(PropertyInfo info)
		{
			_propertyInfo = info;
		}

		public string GetColumnName()
		{
			return ColumnTextUtils.GetColumnName(_propertyInfo);
		}

		public virtual string GetColumnType()
		{
			var sqlDateType = CustomAttributesHelper.GetSqlDateColumnDateType(_propertyInfo);

			string sqlTypeName;
			switch (sqlDateType)
			{
				case SqlDateType.DateTime:
					sqlTypeName = "datetime";
					break;
				case SqlDateType.DateTime2:
					sqlTypeName = "datetime2";
					break;
				case SqlDateType.Date:
					sqlTypeName = "date";
					break;
				case null:
					sqlTypeName = "datetime";
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(sqlDateType));
			}

			return ColumnTextUtils.GetColumnTypeString(sqlTypeName, string.Empty);
		}

		public string GetColumnNullability()
		{
			return ColumnTextUtils.GetColumnNullability(_propertyInfo.PropertyType);
		}
	}
}
