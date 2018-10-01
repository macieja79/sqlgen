using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen.Core.Presentation.Commands
{
    public interface ICommandFactory
    {
        ICommand CreateCommandByName(string name);
    }
}
