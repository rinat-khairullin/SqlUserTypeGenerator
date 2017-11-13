using System;

namespace SqlUserTypeGenerator
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlUserTypeColumnPropertiesAttribute : Attribute
    {
        public int Length { get; }

        public SqlUserTypeColumnPropertiesAttribute(int length)
        {        
            Length = length;
        }

        public const int MaxLength = -1;
    }

}