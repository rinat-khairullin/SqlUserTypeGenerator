using System;

namespace SqlUserTypeGenerator
{
	public class StringHelper
	{
		public static bool IsEqualStrings(string s1, string s2)
		{
			return String.Compare(s1, s2, StringComparison.InvariantCultureIgnoreCase) == 0;
		}

	}
}