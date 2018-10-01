using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class PropertyNameExtractorParameters : ToolParameters
    {
        public string[] Properties { get; set; } = new string[0];
    }
}
