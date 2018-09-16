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

		public static string GetSqlUserTypeTypeName(CustomAttributeData cad)
		{
			return GetAttributeArgumentValue(cad, nameof(SqlUserTypeAttribute.TypeName))?.ToString();
		}

		public static int? GetSqlUserTypeColumnLength(PropertyInfo pi)
		{
			return GetAttributeValue<int?>(pi, nameof(SqlColumnAttribute.Length));
		}

		public static int? GetSqlUserTypeColumnPresicion(PropertyInfo pi)
		{
			return GetAttributeValue<int?>(pi, nameof(SqlColumnAttribute.Presicion));
		}

		public static int? GetSqlUserTypeColumnScale(PropertyInfo pi)
		{
			return GetAttributeValue<int?>(pi, nameof(SqlColumnAttribute.Scale));
		}

		public static bool? GetSqlUserTypeColumnNullable(PropertyInfo pi)
		{
			return GetAttributeValue<bool?>(pi, nameof(SqlColumnAttribute.Nullable));
		}

		public static SqlDateType? GetSqlDateColumnDateType(PropertyInfo pi)
		{
			return GetAttributeValue<SqlDateType?>(pi, nameof(SqlDateColumnAttribute.DateType));
		}

		public static string GetColumnName(PropertyInfo pi)
		{
			return GetAttributeValue<string>(pi, nameof(SqlColumnAttribute.Name));
		}


		private static T GetAttributeValue<T>(PropertyInfo propInfo, string attrName)
		{
			var columnProps = GetColumnProperties(propInfo);
			var attrValue = GetAttributeArgumentValue(columnProps, attrName);
			if (attrValue == null)
				return default(T);

			var propType = TypeHelper.ExtractNonNullableType<T>();
			if (propType.IsEnum)
			{
				return (T)Enum.ToObject(propType, attrValue);
			}

			return (T) Convert.ChangeType(attrValue, propType);
		}

		private static CustomAttributeData GetColumnProperties(PropertyInfo pi)
		{
			var columnProps = GetCustomAttributeByName(pi.GetCustomAttributesData(),
				typeof(SqlColumnAttribute).FullName,
				typeof(SqlDateColumnAttribute).FullName
				);
			return columnProps;
		}

		private static object GetAttributeArgumentValue(CustomAttributeData attr, string argName)
		{
			return attr?.NamedArguments?
				.FirstOrDefault(na => StringHelper.IsEqualStrings(na.MemberName, argName))
				.TypedValue.Value;
		}

		private static CustomAttributeData GetCustomAttributeByName(IList<CustomAttributeData> customAttributes, params string[] attributeTypeFullNames)
		{
			foreach (var cad in customAttributes)
			{
				foreach (var attributeTypeFullName in attributeTypeFullNames)
				{
					if (StringHelper.IsEqualStrings(cad.AttributeType.FullName, attributeTypeFullName))
						return cad;
				}
			}

			return null;
		}
	}
}
