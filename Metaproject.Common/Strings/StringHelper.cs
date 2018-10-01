using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Metaproject
{
    public class StringHelper
    {
        private StringHelper()
        {
        }

        /// <summary>
        /// This metod splits string by separator but omits separators inside quotes
        /// </summary>
        /// <param name="line"></param>
        /// <param name="spearator"></param>
        /// <param name="quotes"></param>
        /// <returns></returns>
        public static List<string> SplitConsiderQuotes(string str, char spearator, char[] quotes)
        {
            if (str.IsNullOrEmpty())
                return new List<string>();

            bool isInsideQuotation = false;
            List<string> items = new List<string>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < str.Length; ++i)
            {
                char c = str[i];
                bool isLastChar = i == str.Length - 1;

                bool isQuoteMark = quotes.Contains(c);
                if (isQuoteMark)
                {
                    isInsideQuotation = !isInsideQuotation;
                    if (isLastChar)
                    {
                        string lastItem = sb.ToString();
                        items.Add(lastItem);
                        break;
                    }

                    continue;
                }

                bool isSeparator = (c == spearator);
                if (isSeparator)
                {
                    if (isInsideQuotation)
                    {
                        sb.Append(c);
                        continue;
                    }

                    string item = sb.ToString();
                    items.Add(item);

                    if (isLastChar)
                    {
                        items.Add("");
                        break;
                    }

                    sb = new StringBuilder();
                    continue;
                }

                sb.Append(c);

                if (isLastChar)
                {
                    string lastItem = sb.ToString();
                    items.Add(lastItem);
                }
            }

            return items;
        }

        public static string ToTittleCase(string input)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            var output = textInfo.ToTitleCase(input.ToLower());
            return output;
        }

        public static List<string> CreateList(params string[] items)
        {
            return items.ToList();
        }

    }
}
