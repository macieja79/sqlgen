using System;
using System.Collections.Generic;

namespace SqlGen
{
    public delegate void CommandSelectedEventHandler(string commandId);
    public delegate void DialogClosedEventHandler(DialogStatus status);

    public interface IUIOutput
    {
        event CommandSelectedEventHandler CommandSelected;
        event DialogClosedEventHandler DialogClosed;
        void ShowOptionForm(Guid commandGuid, object options);
        void AddToOutput(string str);
        void AddToOutput(List<string> lines);
    }
}
