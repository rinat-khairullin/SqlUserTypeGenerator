﻿--autogenerated by SqlUserTypeGenerator v1.0.0.0

create type [t_complex_type_test] as table ( 
	Id int not null,
	Enum int not null,
	NullEnum int null
)
go