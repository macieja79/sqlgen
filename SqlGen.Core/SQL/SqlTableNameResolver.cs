using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common;

namespace SqlGen
{
    public class SqlTableNameResolver : ISqlTableNameResolver
    {
        public string ResolveTableName(string sqlQuery)
        {
            var chunks = sqlQuery.Split(' ').ToList();
            var fromChunk = chunks.Where(c => c.ToUpper() == "FROM").FirstOrDefault();
            var fromChunkIndex = chunks.IndexOf(fromChunk);

            var tableName = chunks[fromChunkIndex + 1];
            return tableName;
        }

        public SqlTableInfo ResolveTableInfo(string sqlQuery)
        {
            string name = ResolveTableName(sqlQuery);

            string trimmed = name.ReplaceMany("", "[", "]");

            string[] chunks = trimmed.Split('.');

            if (chunks.Length == 2)
            {
                return new SqlTableInfo {Name = chunks[1], Schema = chunks[0]};
            }

            return new SqlTableInfo { Name = chunks[0] };
        }
    }
}
