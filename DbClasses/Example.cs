using System;
using SqlUserTypeGenerator;

namespace DbClasses
{
	[SqlUserType(TypeName = "t_example")]
	public class Example
	{
		[SqlColumn(Length = 42)]
		public string NotNullString { get; set; }
		[SqlColumn(Length = 10, Nullable = true)]
		public string NullString { get; set; }
		// string with max length
		[SqlColumn(Length = SqlColumnAttribute.MaxLength)]
		public string StringMax { get; set; }
		[SqlColumn(Presicion = 7, Scale = 3)]
		public decimal Decimal { get; set; }
		[SqlDateColumn(DateType = SqlDateType.DateTime)]
		public DateTime ExplicitDateTime { get; set; }
		[SqlDateColumn(DateType = SqlDateType.DateTime2)]
		public DateTime ExplicitDateTime2 { get; set; }
	}
}
