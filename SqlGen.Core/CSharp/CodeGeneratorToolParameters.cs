﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen.Core.CSharp
{
    public class CodeGeneratorToolParameters : ToolParameters
    {
        public string Header { get; set; }
        public string Pattern { get; set; }
        public string Footer { get; set; }
        public string[,] Data { get; set; }
    }
}
