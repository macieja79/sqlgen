using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject
{
    public class CommandArgsHelper
    {
        public static bool IsArgument(string[] args, string argument)
        {
            try
            {
                foreach (string arg in args)
                {
                   
                    bool isContaining = arg.StartsWith(argument);
                    if (isContaining) return true;
                }
                
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public static bool IsAnyArgument(string[] args, params string[] toCheck)
        {
            try
            {
                foreach (string checkedArg in toCheck)
                {
                    bool isContaining = IsArgument(args, checkedArg);
                    if (isContaining) return true;
                }
            }
            catch
            {
                // ignored
            }

            return false;
        }

        public static string GetArgument(string[] args, string argument)
        {
            try
            {
                foreach (string arg in args)
                {
                    bool isContaining = arg.StartsWith(argument);
                    if (isContaining)
                        return arg;
                }

            }
            catch
            {
                // ignored
            }

            return null;
        }


        public static string GetArgumentValue(string[] args, string argName, char separator)
        {

            string arg = GetArgument(args, argName);
            if (arg.IsNullObj()) return null;

            string[] parts = arg.Split(separator);

            if (parts.Length > 1)
                return parts[1].Trim();

            return null;
        }

    }

}
