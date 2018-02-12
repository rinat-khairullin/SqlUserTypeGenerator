using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SqlUserTypeGenerator;


namespace DbClasses
{
    [SqlUserType(TypeName = "users")]
	[Table("test")]
    public class User : BaseUser
    {
		[JsonIgnore]
		public long PropLong { get; set; }     
		[SqlColumn(Length = 10)]
        public long? PropLongNull { get; set; }
	    [JsonIgnore]
		[SqlColumn(Length = 10)]     
        public string PropString { get; set; }
        public bool PropBool { get; set; }
        public bool? PropBoolNull { get; set; }
        public DateTime PropDateTime { get; set; }
        public DateTime? PropDateTimeNull { get; set; }
	    [SqlColumn(Presicion = 7, Scale = 3)]
		public decimal PropDecimal { get; set; }
	    [SqlColumn(Presicion = 10, Scale = 2)]
		public decimal? PropDecimalNull { get; set; }
	    public decimal DefaultPrecisionNumeric { get; set; }
		public double PropDouble { get; set; }
        public double? PropDoubleNull { get; set; }
        public int PropInt { get; set; }     
        public int? PropIntNull { get; set; }     
        public Guid PropGuid { get; set; }
        public Guid? PropGuidNull { get; set; }
	    [SqlColumn(Nullable = true)]
	    public string NullableString { get; set; }
		public byte[] PropByteArray { get; set; }
        public byte PropByte { get; set; }
        public byte? PropByteNull { get; set; }
		

    }

	//[SqlUserType]
	public class BaseUser
	{
		[SqlColumn(Length = 23)]
		public string BaseProp { get; set; }
	}

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
