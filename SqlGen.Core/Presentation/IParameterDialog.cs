using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public interface IParameterDialog
    {
        void AttachParameters(object parameters);
        event DialogClosedEventHandler DialogClosed;
    }
}
