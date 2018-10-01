using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlGen.Core;

namespace SqlGen
{
    public class SqlQueryEngine : ISqlQueryEngine
    {
        private readonly ISqlTableNameResolver _nameResolver;
        private readonly ISqlCheckIfTableHasIdentity _sqlCheckIfTableHasIdentity;

        public SqlQueryEngine(ISqlTableNameResolver nameResolver, ISqlCheckIfTableHasIdentity sqlCheckIfTableHasIdentity)
        {
            _nameResolver = nameResolver;
            _sqlCheckIfTableHasIdentity = sqlCheckIfTableHasIdentity;
        }

        public SqlQueryResult Query(string connectionString, string selectQuery)
        {
            var result = new SqlQueryResult();

            using (var connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(selectQuery, connection);
                connection.Open();

                var tableName = _nameResolver.ResolveTableName(selectQuery);

                //result.HasIdentity = _sqlCheckIfTableHasIdentity.HasIdentityColumn(connectionString, tableName);
                result.HasIdentity = CheckIfHasIdentity(connection, tableName);

                bool isFirst = true;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int fieldCount = reader.FieldCount;
                    var row = new SqlQueryResultRow();

                    for (int i = 0; i < fieldCount; i++)
                    {
                        if (isFirst)
                        {
                            string columnType = reader.GetDataTypeName(i);
                            result.ColumnTypes.Add(columnType);

                            string columnName = reader.GetName(i);
                            result.ColumnNames.Add(columnName);

                      
                        }

                        object value = reader.GetSqlValue(i);
                        row.Values.Add(value);
                    }

                    isFirst = false;
                    result.Rows.Add(row);
                }
            }

            return result;
        }

        bool CheckIfHasIdentity(SqlConnection connection, string tableName)
        {
            string selectQuery = $"select OBJECTPROPERTY(OBJECT_ID('{tableName}'), 'TableHasIdentity')";

            SqlCommand command = new SqlCommand(selectQuery, connection);

            var objQueryResult = command.ExecuteScalar();

            if (objQueryResult is int)
            {

                int hasIdentityAsInt = (int) objQueryResult;

                bool hasIdentity = hasIdentityAsInt == 1;

                return hasIdentity;

            }

            return false;
        }

    }
}
