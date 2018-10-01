using Metaproject;

namespace SqlGen
{
    public class SqlSnippets : ISqlSnippets
    {



        public SqlScript CreateDropCreateDatabase(string databaseName)
        {
            var sqlBuilder  = new TSqlBuilder();
            sqlBuilder.DropAndRecreateDb(databaseName);

            var script = new SqlScript(sqlBuilder.GetContent());
            return script;
        }

        public SqlScript CreateUseDatabase(string databaseName)
        {
            var sqlBuilder = new TSqlBuilder();
            sqlBuilder.UseDatabase(databaseName);

            var script = new SqlScript(sqlBuilder.GetContent());
            return script;
        }

        public SqlScript CreateSchema(string schema)
        {
            var sqlBuilder = new TSqlBuilder();
            sqlBuilder.CreateSchema(schema);

            var script = new SqlScript(sqlBuilder.GetContent());
            return script;
        }
    }
}
