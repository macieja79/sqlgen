using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common.Practices;
using TreasuryIS.Practices;

namespace SqlGen
{
    
    public class GenerateCsClassCommand : StandardCommand<CsClassGeneratorParameters>
    {
        public GenerateCsClassCommand(IUIOutput uiOutput, ITool<CsClassGeneratorParameters> extractor, IEventAggregator eventAggregator, IProgress progress)
            : base(uiOutput, extractor, eventAggregator, progress)
        {
        }
    }
}
