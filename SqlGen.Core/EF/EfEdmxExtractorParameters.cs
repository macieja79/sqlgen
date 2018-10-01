using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class EfEdmxExtractorParameters : ToolParameters
    {
        public string ConnectionString { get; set; }
        public string Schema { get; set; }
        public string MigrationName { get; set; }
    }
}
