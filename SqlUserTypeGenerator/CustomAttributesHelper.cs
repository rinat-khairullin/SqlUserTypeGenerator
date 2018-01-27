using System;
using System.Linq;
using System.Reflection;

namespace SqlUserTypeGenerator
{
	internal class CustomAttributesHelper
	{
		public static CustomAttributeData GetSqlUserTypeAttributeData(Type type)
		{
			return type.GetCustomAttributesData()
				.FirstOrDefault(cad => StringHelper.IsEqualStrings(cad.AttributeType.FullName, typeof(SqlUserTypeAttribute).FullName));
		}

		public static int? GetSqlUserTypeColumnLength(PropertyInfo pi)
		{
			int? result = null;

			var columnProps = pi.GetCustomAttributesData()
				.FirstOrDefault(cad => StringHelper.IsEqualStrings(cad.AttributeType.FullName, typeof(SqlUserTypeColumnPropertiesAttribute).FullName));

			if (columnProps?.NamedArguments != null)
			{
				var columnLengthPropObject = columnProps.NamedArguments
					.FirstOrDefault(na => StringHelper.IsEqualStrings(na.MemberName,
						nameof(SqlUserTypeColumnPropertiesAttribute.Length))).TypedValue.Value;
				result = Convert.ToInt32(columnLengthPropObject);
			}
			return result;

		}

		public static string GetTypeName(CustomAttributeData cad)
		{
			return cad.NamedArguments?
				.FirstOrDefault(na => StringHelper.IsEqualStrings(na.MemberName, nameof(SqlUserTypeAttribute.TypeName)))
				.TypedValue
				.Value?.ToString();
		}
	}
}