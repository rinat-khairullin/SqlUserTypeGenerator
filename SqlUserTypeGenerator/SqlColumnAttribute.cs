using System;

namespace SqlUserTypeGenerator
{
	[AttributeUsage(AttributeTargets.Property)]
	public class SqlColumnAttribute : Attribute
	{
		public int Scale { get; set; }
		public int Presicion { get; set; }
		public int Length { get; set; }
		public bool Nullable { get; set; }

		public const int MaxLength = -1;

		public string Name { get; set; }
	}
}
