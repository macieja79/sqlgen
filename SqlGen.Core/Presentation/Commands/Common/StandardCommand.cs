using System;
using Metaproject.Common.Practices;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class StandardCommand<TParameters> : ICommand, ISubscriber<OptionsCommitedAggEvent> where TParameters : ToolParameters
    {
        private readonly IUIOutput _uiOutput;
        private readonly ITool<TParameters> _tool;
        private readonly IProgress _progress;
        private TParameters _toolsParameters;

        public StandardCommand(IUIOutput uiOutput, ITool<TParameters> extractor, IEventAggregator eventAggregator, IProgress progress)
        {
            _uiOutput = uiOutput;
            _tool = extractor;
            _progress = progress;
            Id = Guid.NewGuid();

            eventAggregator.SubsribeEvent(this);
        }

        public Guid Id { get; }

        public void Run()
        {
            _toolsParameters = _tool.CreateDefaultParameters();
            _uiOutput.ShowOptionForm(Id, _toolsParameters);
        }

        public void OnEventHandler(OptionsCommitedAggEvent e)
        {


            try
            {
                if (e.CommandId != Id) return;

                var result = _tool.Generate(_toolsParameters);

                _uiOutput.AddToOutput(result.Lines);
                _progress.HideProgress();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
