using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

namespace SqlUserTypeGenerator.Tests
{
	public class Tests
	{
		[Test]
		[TestCaseSource(typeof(ColumnsGenerationTestData), nameof(ColumnsGenerationTestData.TestCases))]
		public bool ColumnsGenerationTest(PropertyInfo pi, string expected)
		{
			var generated = SqlGenerator.CreateSqlColumnString(pi);
			Assert.AreEqual(expected, generated);
			return true;
		}
	}

	public class SourceClass
	{
		public long PropLong { get; set; }
		[SqlColumn(Length = 10)]
		public long? PropLongNull { get; set; }
		[SqlColumn(Length = 10)]
		public string PropString { get; set; }
		[SqlColumn(Length = SqlColumnAttribute.MaxLength)]
		public string PropStringMaxLength { get; set; }
		public bool PropBool { get; set; }
		public bool? PropBoolNull { get; set; }
		public DateTime PropDateTime { get; set; }
		public DateTime? PropDateTimeNull { get; set; }

		//!_! Explicit DateTime Convertion
		[SqlDateColumn(DateType = SqlDateType.DateTime)]
		public DateTime PropExplicitDateTime { get; set; }
		[SqlDateColumn(DateType = SqlDateType.DateTime2)]
		public DateTime PropExplicitDateTime2 { get; set; }
		[SqlDateColumn(DateType = SqlDateType.Date)]
		public DateTime PropExplicitDate { get; set; }

		[SqlDateColumn(DateType = SqlDateType.DateTime)]
		public DateTime? PropExplicitNullableDateTime { get; set; }
		[SqlDateColumn(DateType = SqlDateType.DateTime2)]
		public DateTime? PropExplicitNullableDateTime2 { get; set; }
		[SqlDateColumn(DateType = SqlDateType.Date)]
		public DateTime? PropExplicitNullableDate { get; set; }

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
		public byte[] PropByteArray { get; set; }
		public byte PropByte { get; set; }
		public byte? PropByteNull { get; set; }
		[SqlColumn(Nullable = true)]
		public string NullableString { get; set; }
		[SqlColumn(Nullable = true, Length = 22)]
		public string NullableStringWithLength { get; set; }

		[SqlColumn(Length = SqlColumnAttribute.MaxLength)]
		public byte[] VarbinaryMax { get; set; }

		public FileAccess EnumTest { get; set; }
		public FileAccess? NullEnumTest { get; set; }

		public UserEnum UserEnum { get; set; }

		public IUserInterface UserInterface { get; set; }
		public UserClass UserClass { get; set; }
	}

	public class ColumnsGenerationTestData
	{
		public static IEnumerable TestCases
		{
			get
			{
				return new Dictionary<PropertyInfo, string>
					{
						{GetProperty(nameof(SourceClass.PropLong)), "PropLong bigint not null"},
						{GetProperty(nameof(SourceClass.PropLongNull)), "PropLongNull bigint null"},
						{GetProperty(nameof(SourceClass.PropString)), "PropString nvarchar(10) not null"},
						{GetProperty(nameof(SourceClass.PropStringMaxLength)), "PropStringMaxLength nvarchar(max) not null"},
						{GetProperty(nameof(SourceClass.PropBool)), "PropBool bit not null"},
						{GetProperty(nameof(SourceClass.PropBoolNull)), "PropBoolNull bit null"},

						{GetProperty(nameof(SourceClass.PropDateTime)), "PropDateTime datetime not null"},
						{GetProperty(nameof(SourceClass.PropDateTimeNull)), "PropDateTimeNull datetime null"},

						{GetProperty(nameof(SourceClass.PropExplicitDateTime)), "PropExplicitDateTime datetime not null"},
						{GetProperty(nameof(SourceClass.PropExplicitDateTime2)), "PropExplicitDateTime2 datetime2 not null"},
						{GetProperty(nameof(SourceClass.PropExplicitDate)), "PropExplicitDate date not null"},
						{GetProperty(nameof(SourceClass.PropExplicitNullableDateTime)), "PropExplicitNullableDateTime datetime null"},
						{GetProperty(nameof(SourceClass.PropExplicitNullableDateTime2)), "PropExplicitNullableDateTime2 datetime2 null"},
						{GetProperty(nameof(SourceClass.PropExplicitNullableDate)), "PropExplicitNullableDate date null"},

						{GetProperty(nameof(SourceClass.PropDecimal)), "PropDecimal numeric(7, 3) not null"},
						{GetProperty(nameof(SourceClass.PropDecimalNull)), "PropDecimalNull numeric(10, 2) null"},
						{GetProperty(nameof(SourceClass.DefaultPrecisionNumeric)), "DefaultPrecisionNumeric numeric(18) not null"},
						{GetProperty(nameof(SourceClass.PropDouble)), "PropDouble float not null"},
						{GetProperty(nameof(SourceClass.PropDoubleNull)), "PropDoubleNull float null"},
						{GetProperty(nameof(SourceClass.PropInt)), "PropInt int not null"},
						{GetProperty(nameof(SourceClass.PropIntNull)), "PropIntNull int null"},
						{GetProperty(nameof(SourceClass.PropGuid)), "PropGuid uniqueidentifier not null"},
						{GetProperty(nameof(SourceClass.PropGuidNull)), "PropGuidNull uniqueidentifier null"},
						{GetProperty(nameof(SourceClass.PropByteArray)), "PropByteArray varbinary(50) not null"},
						{GetProperty(nameof(SourceClass.PropByte)), "PropByte tinyint not null"},
						{GetProperty(nameof(SourceClass.PropByteNull)), "PropByteNull tinyint null"},
						{GetProperty(nameof(SourceClass.NullableString)), "NullableString nvarchar(50) null"},
						{GetProperty(nameof(SourceClass.NullableStringWithLength)), "NullableStringWithLength nvarchar(22) null"},
						{GetProperty(nameof(SourceClass.VarbinaryMax)), "VarbinaryMax varbinary(max) not null"},

						{GetProperty(nameof(SourceClass.EnumTest)), "EnumTest int not null"},
						{GetProperty(nameof(SourceClass.NullEnumTest)), "NullEnumTest int null"},
						{GetProperty(nameof(SourceClass.UserEnum)), "UserEnum int not null"},

						{GetProperty(nameof(SourceClass.UserClass)), string.Empty},
						{GetProperty(nameof(SourceClass.UserInterface)), string.Empty},
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
