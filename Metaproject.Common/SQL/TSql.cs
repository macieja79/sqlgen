using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metaproject
{
    public class TSql
    {

        public static string GetAsColumnHeaders(List<string> headers)
        {
            var headerWithBrackets = headers
                .Select(header => $"[{header}]")
                .ToArray();

            var joined = headerWithBrackets.Join(", ");

            var output = $"({joined})";
            return output;
        }

    }
}
