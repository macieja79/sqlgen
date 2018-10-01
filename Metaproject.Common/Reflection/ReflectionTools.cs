using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Metaproject
{
    public class ReflectionTools
    {
        static ReflectionTools()
        {
           
        }

        public static string GetPropertyNamesAsString(object obj, string propertySeparator, string nameValueSeparator)
        {
            var properties = GetPropertyValuesAsStr(obj);
            var namesValues = properties.Select(k => $"{k.Key}{nameValueSeparator}{k.Value}");
            string result = string.Join(propertySeparator, namesValues);
            return result;
        }

        public static List<string> GetPropertyNames(Type objectType)
        {
            PropertyInfo[] infos = objectType.GetProperties();
            var propertyNames = infos
                .Select(i => i.Name)
                .ToList();

            return propertyNames;
        }

        public static string GetPropertyNames(object obj, string separator)
        {
            var props = GetPropertyNames(obj);
            return string.Join(separator, props);
        }

        public static List<string> GetPropertyNames(object obj)
        {
            return GetPropertyNames(obj.GetType());
        }

        public static List<string> GetPropertyValues(object obj)
        {
            List<KeyValuePair<string, string>> kvps = GetPropertyValuesAsStr(obj);
            return kvps
                .Select(k => k.Value)
                .ToList();
        }

        public static string GetPropertyValues(object obj, string separator)
        {
            var props = GetPropertyValues(obj);
            return string.Join(separator, props);
        }

        public static List<KeyValuePair<string, string>> GetPropertyValuesAsStr(object obj)
        {
            Type type = obj.GetType();
            List<KeyValuePair<string, string>> collection = new List<KeyValuePair<string, string>>();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                string name = prop.Name;
                string value = string.Empty;

                object objValue = prop.GetValue(obj, null);
                if (null != objValue)
                {
                    bool isList = prop.IsGenericList();
                    value = isList ? GetValuesFromListAsStr((IList)objValue) : objValue.ToString();
                }

                collection.Add(new KeyValuePair<string, string>(name, value));
            }

            return collection;
        }

        public static List<string> GetValuesFromList(IList iList)
        {
            try
            {
                List<string> items = new List<string>();
                foreach (object t in iList)
                    items.Add(t.ToString());
                return items;
            }
            catch
            {
                return new List<string>();
            }
        }


        public static string GetValuesFromListAsStr(IList iList)
        {
            try
            {
                List<string> items = GetValuesFromList(iList);
                return string.Join(";", items);
            }
            catch
            {
                return "";
            }
        }

        public static string GetPropertyNamesAndValuesAsString(object obj, string nameValueSeparator,  string lineSeparator)
        {
            List<KeyValuePair<string, string>> collection = GetPropertyValuesAsStr(obj);
            List<string> lines = new List<string>();
            foreach (var kvp in collection)
            {
                lines.Add($"{kvp.Key}{nameValueSeparator}{kvp.Value}");
            }

            return string.Join(lineSeparator, lines.ToArray());
        }


        public static List<T> GetAllImplementationsOfInterface<T>(Assembly assembly)
        {
            var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(T))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t);

            List<T> items = new List<T>();
            foreach (var item in instances)
            {
                items.Add((T)item);
            }

            return items;
        }

        public static Version GetExecutingAssemblyVersion()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var version = assemblyName.Version;
            return version;
        }

        public static List<T> GetConsts<T>(Type type)
        {
                return type
                    .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                    .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                    .Select(x => (T)x.GetRawConstantValue())
                    .ToList();
        }

        public static void SetAllValues<T>(object obj, T value)
        {
            foreach (var property in obj.GetType().GetProperties())
            {
                if (property.PropertyType != typeof(T)) continue;
                property.SetValue(obj, value, null);
            }

        }

       
    }
}
