using SqlUserTypeGenerator;

namespace DbClasses
{
	public class BaseUser
	{
		[SqlColumn(Length = 23)]
		public string BaseProp { get; set; }
	}
}