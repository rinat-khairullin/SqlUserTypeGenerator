using System;

namespace SqlUserTypeGenerator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SqlUserTypeAttribute : Attribute
    {
        public string TypeName { get; }

        public SqlUserTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}