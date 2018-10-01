using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class SqlSeedScriptGenerator : ITool<SqlSeedScriptParams>
    {
        private ISqlInsertScriptGenerator _insertScriptGenerator;
        private ISqlCreateScriptGenerator _createScriptGenerator;
        private ISqlSnippets _sqlSnippets;

        public SqlSeedScriptGenerator(
            ISqlInsertScriptGenerator insertScriptGenerator, 
            ISqlCreateScriptGenerator createScriptGenerator, 
            ISqlSnippets sqlSnippets
            )
        {
            _insertScriptGenerator = insertScriptGenerator;
            _createScriptGenerator = createScriptGenerator;
            _sqlSnippets = sqlSnippets;
        }

        public SqlScript Generate(SqlSeedScriptParams parameters)
        {
            var script = new SqlScript();

                if (parameters.IsToRecreateDatabase)
                {
                    var createDbScript = _sqlSnippets.CreateDropCreateDatabase(parameters.TargetDatabaseName);
                    script.Attach(createDbScript);

                    var useDbScript = _sqlSnippets.CreateUseDatabase(parameters.TargetDatabaseName);
                    script.Attach(useDbScript);
                }
                
                if (parameters.IsToGenerateCreateTable)
                {
                    var createScript = _createScriptGenerator.GetCreateTableScript(parameters.ConnectionString, parameters.SqlCommand);
                    script.Attach(createScript);
                }

                if (parameters.IsToGenerateInsertTable)
                {
                var insertParameters = new SqlInsertScriptGeneratorParams
                {
                    ConnectionString = parameters.ConnectionString,
                    SqlSelectStatement = parameters.SqlCommand,
                    ChunkSize = parameters.ChunkSize,
                    MultiStatementSeparator = parameters.MultiStatementSeparator
                };


                var insertScript = _insertScriptGenerator.CreateInsertScript(insertParameters);
                    script.Attach(insertScript);
                }

            return script;
        }

        public SqlSeedScriptParams CreateDefaultParameters()
        {
            return new SqlSeedScriptParams
            {
                ChunkSize = 100,

            };
        }
    }
}
