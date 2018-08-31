## SqlUserTypeGenerator
Generates SQL user-defined table type (UDT) from C# class


For example, for this class:

```csharp
[SqlUserType(TypeName = "t_users")]
public class User
{
	public int Id { get; set; }
	public string Name { get; set; }
	public DateTime? DateOfBirth { get; set; }
}

```

following file will be generated

```sql
--autogenerated by SqlUserTypeGenerator v1.0.0.0

create type [t_users] as table (
	Id int not null,
	Name nvarchar(50) not null,
	DateOfBirth datetime null
)
go

```
### Usage
1. Install package:
```
Install-Package SqlUserTypeGenerator
```
2. add `SqlUserType` attribute with optional sql type name to class

3. build project; new file with sql type definition will be generated to GeneratedSqlTypes folder in project folder

### Types mapping

| .NET type | SQL type           | Note                                    |
|-----------|--------------------|-----------------------------------------|
| Int64     | `bigint`           |                                         |
| string    | `nvarchar`         |                                         |
| Boolean   | `bit`              |                                         |
| DateTime  | `datetime`         | Can be tweaked to `datetime2` or `date` |
| Double    | `float`            |                                         |
| Int32     | `int`              |                                         |
| Decimal   | `numeric`          |                                         |
| Guid      | `uniqueidentifier` |                                         |
| Byte[]    | `varbinary`        |                                         |
| Byte      | `tinyint`          |                                         |
| Enum      | `int`              |                                         |

Complex types (classes and interfaces) are ignored

### Column properties

- Use `SqlColumnAttribute` to define column properties (length and nullability for `nvarchar`,  `precision` and `scale` for numeric).

- Use `SqlDateColumnAttribute` to specify exact sql-type for `DateTime` column (`datetime`, `datetime2` or `date`).

```csharp
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
	[SqlDateColumn(SqlDateType.DateTime)]
	public DateTime ExplicitDateTime { get; set; }
	[SqlDateColumn(SqlDateType.DateTime2)]
	public DateTime ExplicitDateTime2 { get; set; }
	[SqlColumn(Name = "custom_name")]
	public int? CustomName { get; set; }
}
```

```sql
create type [t_example] as table ( 
	NotNullStringField nvarchar(42) not null,
	NullStringField nvarchar(10) null,
	StringMaxField nvarchar(max) not null,
	DecimalField numeric(7, 3) not null,
	ExplicitDateTime datetime not null,
	ExplicitDateTime2 datetime2 not null,
	custom_name int null
)
```

### Notes

* Output folder for generated files can be customized by setting `SqlUserTypeGenerator_TargetFolder` property in csproj file.
* Type pre-create and post-create code can be set via `SqlUserTypeGenerator_TypePreCreateCode` and  `SqlUserTypeGenerator_TypePostCreateCode` properties; `$typename$` string in this code will replaced to generated type name.

(see file `tools\SqlUserTypeGenerator_GlobalProps.props` file in sample DbClassesWithCustomSqlFolder project)
