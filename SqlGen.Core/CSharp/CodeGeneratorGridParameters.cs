﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class CodeGeneratorGridParameters : ToolParameters
    {
        public string[,] Data { get; set; }
    }
}
