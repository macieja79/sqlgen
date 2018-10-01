using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace SqlGen
{
    public class SqlService : ISqlService
    {
        public List<string> GetSchemas(string connectionString)
        {
            var schemas = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string SQL = "select name from sys.schemas;";

                using (SqlCommand command = new SqlCommand(SQL, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            schemas.Add(reader.GetString(0));
                        }
                    }
                }
            }

            return schemas;
        }
    }
}