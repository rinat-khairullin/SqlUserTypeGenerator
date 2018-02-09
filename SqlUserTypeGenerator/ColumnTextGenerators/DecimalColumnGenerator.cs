using System.Globalization;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class DecimalColumnGenerator : ColumnTextGenerator
	{
		public DecimalColumnGenerator(string sqlTypeName, PropertyInfo propertyInfo) : base(sqlTypeName, propertyInfo)
		{
		}

		public override string GetColumnType()
		{
			var presicion = "18";
			var columnLengthString = presicion;
			var precisionFromAttr = CustomAttributesHelper.GetSqlUserTypeColumnPresicion(PropertyInfo);
			if (precisionFromAttr.HasValue)
			{
				presicion = precisionFromAttr.Value.ToString(CultureInfo.InvariantCulture);
				columnLengthString = $"{presicion}";

				var scale = string.Empty;

				var scaleFromAttr = CustomAttributesHelper.GetSqlUserTypeColumnScale(PropertyInfo);
				if (scaleFromAttr.HasValue)
				{
					scale = scaleFromAttr.Value.ToString(CultureInfo.InvariantCulture);
				}
				columnLengthString += !string.IsNullOrEmpty(scale) ? $", {scale}" : string.Empty;
			}

			return GetColumnTypeString(SqlTypeName, columnLengthString);
		}

	}
}