using System.Collections.Generic;
using System.IO;
using SqlUserTypeGenerator;

namespace DbClasses
{
	[SqlUserType(TypeName = "t_complex_type_test")]
	public class ComplexTypeTest
	{
		public int Id { get; set; }
		public User User { get; set; }
		public IList<int> List { get; set; }
		public FileAccess Enum { get; set; }
		public FileAccess? NullEnum { get; set; }

	}
}