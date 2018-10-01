using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen.Core.Presentation.Events
{
    public class ParametersRequestedAggEvent<TParameter> where TParameter : ToolParameters
    {
        public TParameter Parameters { get; set; }
    }
}
