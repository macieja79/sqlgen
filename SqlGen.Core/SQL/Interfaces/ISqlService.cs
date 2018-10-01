
using System.Collections.Generic;

namespace SqlGen
{

    public interface ISqlService
    {
        List<string> GetSchemas(string connectionString);
    }

}