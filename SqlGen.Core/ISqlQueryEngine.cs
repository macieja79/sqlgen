using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen.Core
{
    public interface ISqlQueryEngine
    {
        SqlQueryResult Query(string connectionString, string query);
    }
}
