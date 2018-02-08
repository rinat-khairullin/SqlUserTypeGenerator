using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace SqlUserTypeGenerator.Tests
{
	
	public class Tests
	{
		[Test]
		[TestCaseSource(typeof(ColumnsGenerationTestData), nameof(ColumnsGenerationTestData.TestCases))]
		public bool ColumnsGenerationTest(PropertyInfo pi, string sqlString)
		{
			return string.Compare(sqlString, SqlGenerator.CreateSqlColumnString(pi), StringComparison.InvariantCulture) == 0;			
		}

	}

	public class SourceClass
	{		
		public long PropLong { get; set; }
		[SqlUserTypeColumnProperties(Length = 10)]
		public long? PropLongNull { get; set; }
		[SqlUserTypeColumnProperties(Length = 10)]
		public string PropString { get; set; }
		[SqlUserTypeColumnProperties(Length = SqlUserTypeColumnPropertiesAttribute.MaxLength)]
		public string PropStringMaxLength { get; set; }
		public bool PropBool { get; set; }
		public bool? PropBoolNull { get; set; }
		public DateTime PropDateTime { get; set; }
		public DateTime? PropDateTimeNull { get; set; }
		[SqlUserTypeColumnProperties(Presicion = 7, Scale = 3)]
		public decimal PropDecimal { get; set; }
		[SqlUserTypeColumnProperties(Presicion = 10, Scale = 2)]
		public decimal? PropDecimalNull { get; set; }
		public decimal DefaultPrecisionNumeric { get; set; }
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


	public class ColumnsGenerationTestData
	{
		public static IEnumerable TestCases
		{
			get
			{

				return new Dictionary<PropertyInfo, string>()
					{
						{ GetProperty(nameof(SourceClass.PropLong)), "PropLong bigint not null" },
						{ GetProperty(nameof(SourceClass.PropLongNull)), "PropLongNull bigint null" },
						{ GetProperty(nameof(SourceClass.PropString)), "PropString nvarchar(10) not null" },
						{ GetProperty(nameof(SourceClass.PropStringMaxLength)), "PropStringMaxLength nvarchar(max) not null" },
						{ GetProperty(nameof(SourceClass.PropBool)), "PropBool bit not null" },
						{ GetProperty(nameof(SourceClass.PropBoolNull)), "PropBoolNull bit null" },
						{ GetProperty(nameof(SourceClass.PropDateTime)), "PropDateTime datetime not null" },
						{ GetProperty(nameof(SourceClass.PropDateTimeNull)), "PropDateTimeNull datetime null" },
						{ GetProperty(nameof(SourceClass.PropDecimal)), "PropDecimal numeric(7, 3) not null" },
						{ GetProperty(nameof(SourceClass.PropDecimalNull)), "PropDecimalNull numeric(10, 2) null" },
						{ GetProperty(nameof(SourceClass.DefaultPrecisionNumeric)), "DefaultPrecisionNumeric numeric(18) not null" },
						{ GetProperty(nameof(SourceClass.PropDouble)), "PropDouble float not null" },
						{ GetProperty(nameof(SourceClass.PropDoubleNull)), "PropDoubleNull float null" },
						{ GetProperty(nameof(SourceClass.PropInt)), "PropInt int not null" },
						{ GetProperty(nameof(SourceClass.PropIntNull)), "PropIntNull int null" },
						{ GetProperty(nameof(SourceClass.PropGuid)), "PropGuid uniqueidentifier not null" },
						{ GetProperty(nameof(SourceClass.PropGuidNull)), "PropGuidNull uniqueidentifier null" },
						{ GetProperty(nameof(SourceClass.PropByteArray)), "PropByteArray varbinary not null" },
						{ GetProperty(nameof(SourceClass.PropByte)), "PropByte tinyint not null" },
						{ GetProperty(nameof(SourceClass.PropByteNull)), "PropByteNull tinyint null" },
					}
					.Select(kvp => new TestCaseData(kvp.Key, kvp.Value).Returns(true));
																
			}		
		}

		private static PropertyInfo GetProperty(string propName)
		{
			return typeof(SourceClass).GetProperty(propName);
		}
	}
}
