﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public interface IDialogForm<TParameters> where TParameters : ToolParameters
    {
        TParameters Get();
        void Set(TParameters parameters);
    }
}
