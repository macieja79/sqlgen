using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common.Practices;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class BuildCollectionCommand : StandardCommand<CollectionBuildersParameters>
    {
        public BuildCollectionCommand(IUIOutput uiOutput, ITool<CollectionBuildersParameters> extractor, IEventAggregator eventAggregator, IProgress progress) 
            : base(uiOutput, extractor, eventAggregator, progress)
        {
            
        }
    }
}
