using System.Collections.Generic;

namespace SqlGen.Core
{
    public class SqlQueryResult
    {
        public List<string> ColumnTypes { get; set; } = new List<string>();
        public List<string> ColumnNames { get; set; } = new List<string>();
        public List<SqlQueryResultRow> Rows { get; set; } = new List<SqlQueryResultRow>();
        public bool HasIdentity { get; set; }
    }
}
