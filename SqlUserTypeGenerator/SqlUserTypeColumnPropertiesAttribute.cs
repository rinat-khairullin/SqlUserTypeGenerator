using System;

namespace SqlUserTypeGenerator
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SqlUserTypeColumnPropertiesAttribute : Attribute
    {
        public int Length { get; set; }

        public const int MaxLength = -1;
    }

}