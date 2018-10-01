using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metaproject.Common
{
    public class CodeBuilder
    {

        public static string CreateProperty(string type, string name)
        {
            return $"public {type} {name} {{ get; set; }}";

        }

    }
}
