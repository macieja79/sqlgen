﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
  

    public interface ISqlInsertScriptGenerator
    {
        SqlScript CreateInsertScript(SqlInsertScriptGeneratorParams insertScriptGeneratorParams);
    }
}
