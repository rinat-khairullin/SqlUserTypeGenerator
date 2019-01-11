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
			return GetCustomAttributesByName(type.GetCustomAttributesData(), typeof(SqlUserTypeAttribute).FullName).FirstOrDefault();
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
			foreach (var customAttributeData in columnProps)
			{
				var attrValue = GetAttributeArgumentValue(customAttributeData, attrName);
				if (attrValue == null)
					continue;

				var propType = TypeHelper.ExtractNonNullableType<T>();
				if (propType.IsEnum)
				{
					return (T)Enum.ToObject(propType, attrValue);
				}

				return (T)Convert.ChangeType(attrValue, propType);
			}

			return default(T);
		}

		private static IList<CustomAttributeData> GetColumnProperties(PropertyInfo pi)
		{
			return GetCustomAttributesByName(pi.GetCustomAttributesData(),
				typeof(SqlColumnAttribute).FullName,
				typeof(SqlDateColumnAttribute).FullName
			);
		}

		private static object GetAttributeArgumentValue(CustomAttributeData attr, string argName)
		{
			return attr?.NamedArguments?
				.FirstOrDefault(na => StringHelper.IsEqualStrings(na.MemberName, argName))
				.TypedValue.Value;
		}

		private static IList<CustomAttributeData> GetCustomAttributesByName(IList<CustomAttributeData> customAttributes, params string[] attributeTypeFullNames)
		{
			var result = new List<CustomAttributeData>();
			foreach (var cad in customAttributes)
			{
				foreach (var attributeTypeFullName in attributeTypeFullNames)
				{
					if (StringHelper.IsEqualStrings(cad.AttributeType.FullName, attributeTypeFullName))
						result.Add(cad);
				}
			}

			return result;
		}
	}
}
