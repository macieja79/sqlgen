using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

namespace SqlGen
{
   

    public class EfEdmxExtractor : ITool<EfEdmxExtractorParameters>
    {
        public EfEdmxExtractorParameters CreateDefaultParameters()
        {
           return  new EfEdmxExtractorParameters();
        }

        public SqlScript Generate(EfEdmxExtractorParameters parameters)
        {
            var sqlToExecute = $"select Model from [{parameters.Schema}].[_MigrationHistory] where MigrationId like '%{parameters.MigrationName}'";

            using (var connection = new SqlConnection(parameters.ConnectionString))
            {
                connection.Open();

                var command = new SqlCommand(sqlToExecute, connection);

                var reader = command.ExecuteReader();
                if (!reader.HasRows)
                {
                    throw new Exception("Now Rows to display. Probably migration name is incorrect");
                }

                while (reader.Read())
                {
                    var model = (byte[])reader["model"];
                    var decompressed = Decompress(model);
                    var content = decompressed.ToString();
                    return new SqlScript{Lines = content.ToSingleElementList()};
                }
            }

            throw new Exception("Something went wrong. You should not get here");
        }

        
        
        /// <summary>
        /// Stealing decomposer from EF itself:
        /// http://entityframework.codeplex.com/SourceControl/latest#src/EntityFramework/Migrations/Edm/ModelCompressor.cs
        /// </summary>
        private XDocument Decompress(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                using (var gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    return XDocument.Load(gzipStream);
                }
            }
        }
        
       
    }
}
