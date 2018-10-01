using System.Collections.Generic;

namespace SqlGen
{
    public class CsClassGeneratorParameters : ToolParameters
    {
        public string TargetClassName { get;  set; }
        public string[] PropertyNames { get; set; }
    }
}
