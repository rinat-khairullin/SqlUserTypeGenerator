using System;

namespace SqlUserTypeGenerator
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SqlDateColumnAttribute : Attribute
	{
		public SqlDateType DateType { get; set; }
	}
}