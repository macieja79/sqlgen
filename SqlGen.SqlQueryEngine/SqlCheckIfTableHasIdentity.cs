using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class SqlCheckIfTableHasIdentity : ISqlCheckIfTableHasIdentity
    {
        private readonly ISqlCreateScriptGenerator _sqlCreateScriptGenerator;

        public SqlCheckIfTableHasIdentity(ISqlCreateScriptGenerator sqlCreateScriptGenerator)
        {
            _sqlCreateScriptGenerator = sqlCreateScriptGenerator;
        }

        public bool HasIdentityColumn(string connectionString, string tableName)
        {
            var sql = $"select * from {tableName}";

            var script = _sqlCreateScriptGenerator
                .GetCreateTableScript(connectionString, sql.ToSingleElementArray());

            bool hasIdentity = script.Lines.Any(l => l.Contains(" IDENITTY "));

            return hasIdentity;

        }
    }
}
