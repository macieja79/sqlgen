using System;
using System.Diagnostics;

namespace Metaproject
{
    public class OutputLogger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }

        public void Log(Exception exception)
        {
            Log(exception.Message);
        }
    }
}
