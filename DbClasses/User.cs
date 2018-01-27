using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using SqlUserTypeGenerator;


namespace DbClasses
{
    [SqlUserType("users")]
	[Table("test")]
    public class User : BaseUser
    {
		[JsonIgnore]
		public long PropLong { get; set; }     
        public long? PropLongNull { get; set; }
	    [JsonIgnore]
		[SqlUserTypeColumnProperties(43)]     
        public string PropString { get; set; }
        public bool PropBool { get; set; }
        public bool? PropBoolNull { get; set; }
        public DateTime PropDateTime { get; set; }
        public DateTime? PropDateTimeNull { get; set; }
        public decimal PropDecimal { get; set; }
        public decimal? PropDecimalNull { get; set; }
        public double PropDouble { get; set; }
        public double? PropDoubleNull { get; set; }
        public int PropInt { get; set; }     
        public int? PropIntNull { get; set; }     
        public Guid PropGuid { get; set; }
        public Guid? PropGuidNull { get; set; }
        public byte[] PropByteArray { get; set; }
        public byte PropByte { get; set; }
        public byte? PropByteNull { get; set; }
    }

	[SqlUserType("base_user")]
	public class BaseUser
	{
		[SqlUserTypeColumnProperties(23)]
		public string BaseProp { get; set; }
	}
}
