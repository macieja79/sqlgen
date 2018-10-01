using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class CollectionBuildersParameters : ToolParameters
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Separator { get; set; }
        public string ItemPattern { get; set; }
        public string[] Items { get; set; }
    }
}
