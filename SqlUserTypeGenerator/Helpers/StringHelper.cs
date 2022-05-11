﻿using System;
using System.Text;

namespace SqlUserTypeGenerator.Helpers
{
	internal static class StringHelper
	{
		public static bool IsEqualStrings(string s1, string s2)
		{
			return string.Compare(s1, s2, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		public static string EncodeArgument(string arg)
		{
			return !string.IsNullOrEmpty(arg) ? Convert.ToBase64String(Encoding.UTF8.GetBytes(arg)) : null;
		}

		public static string DecodeArgument(string arg)
		{
			return !string.IsNullOrEmpty(arg) ? Encoding.UTF8.GetString(Convert.FromBase64String(arg)) : null;
		}

		public static bool CustomParseBool(string boolString)
		{
			int i;
			if (int.TryParse(boolString, out i))
			{
				return Convert.ToBoolean(i);
			}

			return Convert.ToBoolean(boolString);
		}
	}
}
