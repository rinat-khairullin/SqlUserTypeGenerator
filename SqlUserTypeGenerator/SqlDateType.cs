namespace SqlUserTypeGenerator
{
	/// <summary> Possible datetime sql-types </summary>
	public enum SqlDateType
	{
		/// <summary> Value to generate 'datetime' sql-type </summary>
		DateTime = 0,

		/// <summary> Value to generate 'datetime2' sql-type </summary>
		DateTime2 = 1,

		/// <summary> Value to generate 'date' sql-type </summary>
		Date = 2,
	}
}
