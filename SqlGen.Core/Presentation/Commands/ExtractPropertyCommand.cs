using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class ExtractPropertyCommand : StandardCommand<CsClassGeneratorParameters>
    {
        public ExtractPropertyCommand(IUIOutput uiOutput, ITool<CsClassGeneratorParameters> extractor, IEventAggregator eventAggregator, IProgress progress)
            : base(uiOutput, extractor, eventAggregator, progress)
        {
        }
    }

}
