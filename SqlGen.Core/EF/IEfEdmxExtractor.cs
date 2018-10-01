using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public interface IEfEdmxExtractor : ITool<EfEdmxExtractorParameters>
    {
        string GetEdmx(EfEdmxExtractorParameters efEdmxExtractorParameters);
        string GetEdmxFromLastMigration(string connectionString, string schema);
    }
}
