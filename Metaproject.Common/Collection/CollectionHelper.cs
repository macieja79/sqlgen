using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    public class CollectionHelper
    {
        public static Dictionary<string, string> CreateStringDictionary(params string[] namesAndValues)
        {
            var dictionary = new Dictionary<string, string>();
            for (int i = 0; i < namesAndValues.Length; i = i + 2)
            {
                string key = namesAndValues[i];
                string value = namesAndValues[i + 1];
                dictionary[key] = value;
            }

            return dictionary;

        }

        public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(params object[] namesAndValues)
        {
            var dictionary = new Dictionary<TKey, TValue>();
            for (int i = 0; i < namesAndValues.Length; i = i + 2)
            {
                TKey key = (TKey)namesAndValues[i];
                TValue value = (TValue)namesAndValues[i + 1];
                dictionary[key] = value;
            }

            return dictionary;
        }

    }
}
