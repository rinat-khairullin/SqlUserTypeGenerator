using System;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class DateColumnGenerator : IColumnTextGenerator
	{
		private readonly PropertyInfo _propertyInfo;
		private GenerateUserTypeSettings _settings;

		public DateColumnGenerator(PropertyInfo info, GenerateUserTypeSettings settings)
		{
			_propertyInfo = info;
			_settings = settings;
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
					sqlTypeName = _settings.UseSqlDateTime2 ? "datetime2" : "datetime";
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
