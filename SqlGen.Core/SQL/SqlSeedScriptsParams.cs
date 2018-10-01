using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class SqlSeedScriptParams : ToolParameters
    {
        public string ConnectionString { get; set; }
        
        public int ChunkSize { get; set; }

        public bool IsToRecreateDatabase { get; set; }
        public bool IsToGenerateCreateTable { get; set; }
        public bool IsToGenerateInsertTable { get; set; }
        public string TargetDatabaseName { get; set; }

        public string MultiStatementSeparator { get; set; }

        public string[] SqlCommand { get; set; } = new string[0];
    }
}
