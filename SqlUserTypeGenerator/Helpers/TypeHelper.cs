using System;
using System.Reflection;

namespace SqlUserTypeGenerator.Helpers
{
	internal class TypeHelper
	{
		internal static Type ExtractNonNullableType<T>()
		{
			var type = typeof(T);
			return GetNonNullableType(type) ?? type;
		}

		internal static Type ExtractNonNullableType(PropertyInfo pi)
		{
			return GetNonNullableType(pi.PropertyType) ?? pi.PropertyType;
		}

		internal static Type GetNonNullableType(Type propertyType)
		{
			return Nullable.GetUnderlyingType(propertyType);
		}
		internal static bool IsNullableType(Type propType)
		{
			return GetNonNullableType(propType) != null;
		}
	}
}
