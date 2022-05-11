using System;

namespace SqlUserTypeGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlUserTypeAttribute : Attribute
    {
		/// <summary> Name of sql user type </summary>
        public string TypeName { get; set; }
    }
}
