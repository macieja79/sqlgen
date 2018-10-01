using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common;

namespace SqlGen
{
    public interface ISqlTableNameResolver
    {
        string ResolveTableName(string sqlQuery);
        SqlTableInfo ResolveTableInfo(string sqlQuery);

    }
}
