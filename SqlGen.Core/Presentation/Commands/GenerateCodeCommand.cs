using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metaproject.Common.Practices;
using SqlGen.Core.CSharp;
using SqlGen.Core.Presentation.Events;
using TreasuryIS.Practices;

namespace SqlGen
{
    public class GenerateCodeCommand : ICommand, ISubscriber<OptionsCommitedAggEvent> 
    {
        private readonly IUIOutput _uiOutput;
        private readonly ITool<CodeGeneratorToolParameters> _codeGenerator;
        private readonly IEventAggregator _eventAggregator;


        public GenerateCodeCommand(IUIOutput uiOutput, ITool<CodeGeneratorToolParameters> codeGenerator, IEventAggregator eventAggregator)
        {
            _uiOutput = uiOutput;
            _codeGenerator = codeGenerator;
            _eventAggregator = eventAggregator;

            Id = Guid.NewGuid();

            _eventAggregator.SubsribeEvent(this);
        }

        public Guid Id { get; }
        public void Run()
        {
            var parameters = _codeGenerator.CreateDefaultParameters();
            _uiOutput.ShowOptionForm(Id, parameters);    
        }

        public void OnEventHandler(OptionsCommitedAggEvent e)
        {
            var codeParameters = new CodeGeneratorParameters();
            _eventAggregator.PublishEvent(new ParametersRequestedAggEvent<CodeGeneratorParameters>{Parameters = codeParameters});

            var codeGridParameters = new CodeGeneratorGridParameters();
            _eventAggregator.PublishEvent(new ParametersRequestedAggEvent<CodeGeneratorGridParameters> { Parameters = codeGridParameters });

            var codeGeneratorParams = new CodeGeneratorToolParameters
            {
                Header = codeParameters.Header,
                Pattern = codeParameters.Pattern,
                Footer =  codeParameters.Footer,
                Data = codeGridParameters.Data
            };
            
            var script = _codeGenerator.Generate(codeGeneratorParams);
            _uiOutput.AddToOutput(script.Lines);
        }
    }
}
