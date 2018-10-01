using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace System
{
    public static class StringsExtensions
    {
        public static List<int> ToInt(this List<string> list)
        {
            var result = list.Select(i => i.ToInt()).ToList();
            return result;
        }

        public static string Quoted(this string str)
        {
            return $"\"{str}\"";
        }

        public static string Join(this IEnumerable<string> input, string separator)
        {
            if (input.IsNullObj()) return string.Empty;
            string output = string.Join(separator, input);
            return output;
        }

        public static string Joined(this IEnumerable<string> input)
        {
            if (input.IsNullObj()) return string.Empty;
            string output = string.Join("; ", input);
            return output;
        }

        public static List<string> SplitWithManySeparators(this string str, params char[] separators)
        {
            if (str.IsNullOrEmpty()) return new List<string>();

            int matched = 0;
            char matchedSeparator = char.MinValue;
            foreach (char separator in separators)
            {
                if (str.Contains(separator.ToString()))
                {
                    matched++;
                    matchedSeparator = separator;
                }
            }

            if (matched != 1)
            {
                return new List<string>() {str};
            }

            string[] items = str.Split(matchedSeparator);
            return items.ToList();
        }

        public static int ToInt(this string str0)
        {
            string str = str0.AdjustSeparator();

            int intValue = 0;

            if (double.TryParse(str, out double doubleValue))
            {
                intValue = (int)doubleValue;
                return intValue;
            }
            else if (int.TryParse(str, out intValue))
            {
                return intValue;
            }

            return intValue;
        }

        public static string AdjustSeparator(this string str)
        {
            var separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            var str0 = str.Replace(",", separator);
            var str1 = str0.Replace(".", separator);
            return str1;
        }

        public static string ReplaceSafe(this string str, string src, string dest)
        {
            if (str.IsNullOrEmpty()) return str;
            return str.Replace(src, dest);
        }

        public static string ReplaceMany(this string str, string target, params string[] srcs)
        {
            var copy = str.ToString();
            foreach (var src in srcs)
                copy = copy.Replace(src, target);

            return copy;
        }


        public static string[] SplitAndTrim(this string str, char separator)
        {
            if (str.IsNullOrEmpty()) return new string[] { };

            string[] output = str.Split(separator)
                .Select(item => item.Trim())
                .ToArray();

            return output;
        }

        public static string GetUpToSymbol(this string str, string c)
        {
            int index = str.IndexOf(c);
            int length = index;
            string output = str.Substring(0, length);
            return output;
        }

        public static string TrimSafely(this string input)
        {
            if (input.IsNullOrEmpty()) return input;
            string output = input.Trim();
            return output;
        }

        public static string[] SplitSafely(this string input, string separator)
        {
            if (input.IsNullOrEmpty()) return new string[0];
            if (separator.IsNullOrEmpty()) return new string[0];

            var items = input.Split(separator.ToArray());
            return items;
        }

        public static bool ContainsMany(this string str, params string[] args)
        {
            foreach (var arg in args)
            {
                if (!str.Contains(arg)) return false;
            }

            return true;
        }

        public static bool ContainsAny(this string str, params string[] args)
        {
            foreach (var arg in args)
            {
                if (str.Contains(arg)) return true;
            }

            return false;
        }

        public static bool IsOneOf(this string str, params string[] args)
        {
            if (str.IsNullOrEmpty()) return false;

            foreach (var arg in args)
            {
                if (str == arg) return true;
            }

            return false;
        }

        public static void AppendLines(this StringBuilder sb, params string[] lines)
        {
            foreach (string line in lines)
                sb.AppendLine(line);

        }


    }
}