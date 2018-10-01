using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen.Core.CSharp
{
    public class CodeGeneratorParameters : ToolParameters
    {
        public string Header { get; set; }
        public string Pattern { get; set; }
        public string Footer { get; set; }
    }
}
