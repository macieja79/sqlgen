using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using SqlGen.Core;


namespace SqlGen
{
    public class CreateTableScriptGenerator : ISqlCreateScriptGenerator
    {
        private readonly ISqlTableNameResolver _nameResolver;
        private readonly IIgnoredQuerySpecification _ignoredQuerySpecification;
        private readonly ISqlService _sqlService;
        private readonly ISqlSnippets _snippets;
        private readonly IProgress _progress;

        public CreateTableScriptGenerator(ISqlTableNameResolver nameResolver, IIgnoredQuerySpecification ignoredQuerySpecification, ISqlService sqlService, ISqlSnippets snippets, IProgress progress)
        {
            _nameResolver = nameResolver;
            _ignoredQuerySpecification = ignoredQuerySpecification;
            _sqlService = sqlService;
            _snippets = snippets;
            _progress = progress;
        }

        public SqlScript GetCreateTableScript(string connectionString, string[] sqls)
        {
            var builder = new SqlConnectionStringBuilder(connectionString);
            var databaseName = builder.InitialCatalog;
            
            string sqlTemplate = Resources.GenerateCreateTable;

            var script = new SqlScript();

            var schemasToCreate = GetSchemasToCreate(connectionString, sqls);

            foreach (var schema in schemasToCreate)
            {
                var iScript = _snippets.CreateSchema(schema);
                script.Attach(iScript);
            }
           

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < sqls.Length; i++)
                {
                    var iSql = sqls[i];
                    if (iSql.IsNullOrEmpty()) continue;

                    if (_ignoredQuerySpecification.IsToIgnore(iSql))
                        continue;

                    var tableName = _nameResolver.ResolveTableName(iSql).Replace("[", "").Replace("]", "");

                    _progress.Show($"Create {tableName}", i, sqls.Length);

                    string sqlCommand = sqlTemplate
                        .Replace("%TABLENAME%", tableName)
                        .Replace("%DATABASENAME%", databaseName);

                    SqlCommand command = new SqlCommand(sqlCommand, connection);

                    var objResult = command.ExecuteScalar();

                    if (objResult is string)
                    {
                        var separatorArray = new[] {"\r"};

                        string result = (string) objResult;
                        var iScript = new SqlScript
                        {
                            TargetTable = tableName,
                            Lines = result.Split(separatorArray, StringSplitOptions.RemoveEmptyEntries).ToList()
                        };

                        iScript.Lines.Add("");
                        iScript.Lines.Add("GO");
                        script.Lines.AddRange(iScript.Lines);
                    }

                    
                }


                return script;
            }

        }

        List<string> GetSchemasToCreate(string connectionString,  string[] sqls)
        {
            var schemas = new List<string>();
            foreach (var sql in sqls)
            {
                if (sql.IsNullOrEmpty())
                    continue;

                var tableInfo = _nameResolver.ResolveTableInfo(sql);
                schemas.Add(tableInfo.Schema);
            }

            var distincted = schemas.Distinct().ToList();
            return distincted;
            
        }
    }
}
