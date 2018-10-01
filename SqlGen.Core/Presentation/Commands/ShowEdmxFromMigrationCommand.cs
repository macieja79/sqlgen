using TreasuryIS.Practices;

namespace SqlGen
{
    public class ShowEdmxFromMigrationCommand : StandardCommand<EfEdmxExtractorParameters>
    {
        public ShowEdmxFromMigrationCommand(IUIOutput uiOutput, ITool<EfEdmxExtractorParameters> extractor, IEventAggregator eventAggregator, IProgress progress)
            : base(uiOutput, extractor, eventAggregator, progress)
        {
        }
    }
}
