using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Metaproject
{
    public static class RegexHelper
    {


        public static bool Match(string input, string regexPattern, out Dictionary<string, string> dictionary)
        {

            Regex regex = new Regex(regexPattern, RegexOptions.Compiled | RegexOptions.Multiline);
            MatchCollection matchCollection = regex.Matches(input);

            dictionary = new Dictionary<string, string>();
            bool isSuccess = false;
            foreach (Match match in matchCollection)
            {
                if (match.Success)
                    isSuccess = true;

                for (int i = 0; i < match.Groups.Count; i++)
                {
                    Group iGroup = match.Groups[i];
                    string name = regex.GroupNameFromNumber(i);
                    string value = iGroup.Value;
                    dictionary[name] = value;
                }
            }

            return isSuccess;
        }

    }
}
