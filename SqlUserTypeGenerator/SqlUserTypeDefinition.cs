using System.Collections.Generic;

namespace SqlUserTypeGenerator
{
    internal class SqlUserTypeDefinition
    {
        public string TypeName { get; set; }
        public IList<string> Columns { get; set; }
    }
}