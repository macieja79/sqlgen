using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public class RevertLinesTool : ITool<ReverseLinesToolsParameters>
    {
        public ReverseLinesToolsParameters CreateDefaultParameters()
        {
            return new ReverseLinesToolsParameters();
        }

        public SqlScript Generate(ReverseLinesToolsParameters parameters)
        {

            var lines = parameters.Lines;

            var reverted = lines.Reverse();

            return new SqlScript{Lines = reverted.ToList()};
        }
    }
}
