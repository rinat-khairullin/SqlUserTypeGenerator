using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Build.Framework;

namespace SqlUserTypeGenerator.Helpers
{
	internal static class FaultHelper
	{
		public const string Code = "SUTG";
		public const string GeneralError = "SUTG:General-Error";
		public const string AssemblyLoadError = "SUTG:AssemblyLoad-Error";
		public const string Separator = " -----> ";

		public static string ToInline(this IList<Exception> items, string separator)
		{
			if (items == null) return null;

			return string.Join(
				separator,
				items.Select(x => x.ToInline())
			);
		}

		public static string ToInline(this Exception exc)
		{
			return exc?.ToString()
				.Replace("\n\r", " ")
				.Replace("\r\n", " ")
				.Replace("\n", " ")
				.Replace("\r", " ");
		}

		public static string GetMessage(ReflectionTypeLoadException exc)
		{
			return
				$"Exc: {exc.ToInline()}{Separator}" +
				$"LoaderExc: {exc.LoaderExceptions.ToInline(Separator)}";
		}

		public static string GetMessageCore(Exception exc)
		{
			return $"Exc: {exc.ToInline()}";
		}
		public static BuildErrorEventArgs CreateErrorEvent(string errorCode, string file, string msg)
		{
			return new BuildErrorEventArgs(
				Code,
				errorCode,
				file,
				1, 1, 1, 1,
				msg,
				Code,
				Code
			);
		}
	}
}
