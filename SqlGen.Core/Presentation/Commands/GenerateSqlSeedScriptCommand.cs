using Metaproject.Common.Practices;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class GenerateSqlSeedScriptCommand : StandardCommand<SqlSeedScriptParams>
    {
        public GenerateSqlSeedScriptCommand(IUIOutput uiOutput, ITool<SqlSeedScriptParams> extractor, IEventAggregator eventAggregator, IProgress progress)
            : base(uiOutput, extractor, eventAggregator, progress)
        {

        }
    }
}
