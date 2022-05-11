using System;

namespace SqlUserTypeGenerator
{
	/// <summary> Attribute for sql-column customization </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class SqlColumnAttribute : Attribute
	{
		/// <summary> Constant to use for specifying length of varchar(max) or nvarchar(max) </summary>
		public const int MaxLength = -1;

		/// <summary> Name of sql-column, if set </summary>
		public string Name { get; set; }

		/// <summary> Sql-column length, applicable for string data-types </summary>
		public int Length { get; set; }

		/// <summary> Sql-column nullability </summary>
		public bool Nullable { get; set; }

		/// <summary> Sql-column scale, applicable for datetime, float and double data-types </summary>
		public int Scale { get; set; }

		/// <summary> Sql-column precision, applicable for datetime, float and double data-types </summary>
		public int Presicion { get; set; }
	}
}
