using System.Globalization;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class DecimalColumnGenerator : IColumnTextGenerator
	{
		private readonly string _sqlTypeName;
		private readonly PropertyInfo _propertyInfo;

		public DecimalColumnGenerator(string sqlTypeName, PropertyInfo propertyInfo)
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
			var presicion = "18";
			var columnLengthString = presicion;
			var precisionFromAttr = CustomAttributesHelper.GetSqlUserTypeColumnPresicion(_propertyInfo);
			if (precisionFromAttr.HasValue)
			{
				presicion = precisionFromAttr.Value.ToString(CultureInfo.InvariantCulture);
				columnLengthString = $"{presicion}";

				var scale = string.Empty;

				var scaleFromAttr = CustomAttributesHelper.GetSqlUserTypeColumnScale(_propertyInfo);
				if (scaleFromAttr.HasValue)
				{
					scale = scaleFromAttr.Value.ToString(CultureInfo.InvariantCulture);
				}
				columnLengthString += !string.IsNullOrEmpty(scale) ? $", {scale}" : string.Empty;
			}

			return ColumnTextUtils.GetColumnTypeString(_sqlTypeName, columnLengthString);
		}

		public string GetColumnNullability()
		{
			return ColumnTextUtils.GetColumnNullability(_propertyInfo.PropertyType);
		}
	}
}