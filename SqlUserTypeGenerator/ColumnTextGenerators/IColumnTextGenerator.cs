namespace SqlUserTypeGenerator.ColumnTextGenerators
{
	internal interface IColumnTextGenerator
	{
		string GetColumnName();
		string GetColumnType();		
		string GetColumnNullability();
	}
}