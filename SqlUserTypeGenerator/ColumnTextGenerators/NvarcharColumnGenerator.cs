using System.Globalization;
using System.Reflection;
using SqlUserTypeGenerator.Helpers;

namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal class NvarcharColumnGenerator : ColumnTextGenerator
	{
		public NvarcharColumnGenerator(string sqlTypeName, PropertyInfo propertyInfo) : base(sqlTypeName, propertyInfo)
		{
		}

		public override string GetColumnType()
		{
			var columnLengthString = "50";			
			var columnLengthFromAttr = CustomAttributesHelper.GetSqlUserTypeColumnLength(PropertyInfo);
			if (columnLengthFromAttr.HasValue)
			{
				columnLengthString = columnLengthFromAttr.Value == SqlUserTypeColumnPropertiesAttribute.MaxLength ? "max" : columnLengthFromAttr.Value.ToString(CultureInfo.InvariantCulture);
			}

			return GetColumnTypeString(SqlTypeName, columnLengthString);			
		}

	}
}