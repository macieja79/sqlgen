using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlGen.Core.Presentation;
using SqlGen.Core.Presentation.Commands;

namespace SqlGen
{
    public class UiController : IUIController
    {
        private readonly ICommandFactory _factory;
        

        public UiController(ICommandFactory factory)
        {
            _factory = factory;
            
        }

        public void CommandSelected(string commandName)
        {
            var command = _factory.CreateCommandByName(commandName);
            command.Run();
        }
    }
}
