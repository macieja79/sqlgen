using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SqlGen
{
    public class IgnoredQuerySpecification : IIgnoredQuerySpecification
    {
        public bool IsToIgnore(string sql)
        {
            string trimmed = sql.Trim();
            if (trimmed.StartsWith("--")) return true;

            return false;
        }
    }
}
