using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SqlUserTypeGenerator.Helpers
{
	internal class CustomAttributesHelper
	{
		public static CustomAttributeData GetSqlUserTypeAttributeData(Type type)
		{
			return GetCustomAttributeByName(type.GetCustomAttributesData(), typeof(SqlUserTypeAttribute).FullName);			
		}

		public static int? GetSqlUserTypeColumnLength(PropertyInfo pi)
		{
			return AttributeValueGetter(pi, nameof(SqlUserTypeColumnPropertiesAttribute.Length));			
		}

		public static int? GetSqlUserTypeColumnPresicion(PropertyInfo pi)
		{
			return AttributeValueGetter(pi, nameof(SqlUserTypeColumnPropertiesAttribute.Presicion));			
		}

		public static int? GetSqlUserTypeColumnScale(PropertyInfo pi)
		{
			return AttributeValueGetter(pi, nameof(SqlUserTypeColumnPropertiesAttribute.Scale));
		}


		private static Func<PropertyInfo, string, int?> AttributeValueGetter => (propInfo, attrName) =>
		{
			var columnProps = GetColumnProperties(propInfo);

			var columnLengthPropObject = GetAttributeArgumentValue(columnProps, attrName);

			return columnLengthPropObject != null ? Convert.ToInt32(columnLengthPropObject) : default(int?);
		};

		private static CustomAttributeData GetColumnProperties(PropertyInfo pi)
		{
			var columnProps = GetCustomAttributeByName(pi.GetCustomAttributesData(),
				typeof(SqlUserTypeColumnPropertiesAttribute).FullName);
			return columnProps;
		}


		private static object GetAttributeArgumentValue(CustomAttributeData attr, string argName)
		{
			return attr?.NamedArguments?
				.FirstOrDefault(na => StringHelper.IsEqualStrings(na.MemberName, argName))
				.TypedValue.Value;
		}

		private static CustomAttributeData GetCustomAttributeByName(IList<CustomAttributeData> customAttributes, string attrName)
		{
			return
				customAttributes
				.FirstOrDefault(cad => StringHelper.IsEqualStrings(cad.AttributeType.FullName, attrName));			
		}


		public static string GetTypeName(CustomAttributeData cad)
		{
			return GetAttributeArgumentValue(cad, nameof(SqlUserTypeAttribute.TypeName))?.ToString();				
		}
	}
}