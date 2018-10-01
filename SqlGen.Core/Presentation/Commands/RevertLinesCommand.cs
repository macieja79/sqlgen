using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class RevertLinesCommand : StandardCommand<ReverseLinesToolsParameters>
    {
        public RevertLinesCommand(IUIOutput uiOutput, ITool<ReverseLinesToolsParameters> extractor, IEventAggregator eventAggregator, IProgress progress)
            : base(uiOutput, extractor, eventAggregator, progress)
        {

        }
    }
}
