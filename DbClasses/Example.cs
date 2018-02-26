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
		[SqlColumn(Length = SqlColumnAttribute.MaxLength)]
		public string StringMax { get; set; }
		[SqlColumn(Presicion = 7, Scale = 3)]
		public decimal Decimal { get; set; }
	}
}