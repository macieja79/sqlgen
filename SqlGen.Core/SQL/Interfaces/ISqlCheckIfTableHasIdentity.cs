using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public interface ISqlCheckIfTableHasIdentity
    {
        bool HasIdentityColumn(string connectionString, string tableName);
    }
}
