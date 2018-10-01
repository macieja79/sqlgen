using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public interface ISqlCreateScriptGenerator
    {

        SqlScript GetCreateTableScript(string connectionString, string[] sql);
    }
}
