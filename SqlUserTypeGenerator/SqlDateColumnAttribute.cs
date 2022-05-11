using System;

namespace SqlUserTypeGenerator
{
	/// <summary> Attribute to customize generation of DateTime columns </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class SqlDateColumnAttribute : Attribute
	{
		/// <summary> Type of specific sql data type </summary>
		public SqlDateType DateType { get; set; }
	}
}
