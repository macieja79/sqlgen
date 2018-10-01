using System.Linq;

namespace System.Collections.Generic
{
    public static class CollectionExtensions
    {
        public static void RemoveWhere<T>(this List<T> list, Predicate<T> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                bool isFulfiled = predicate(list[i]);
                if (isFulfiled)
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }

        public static List<object> ToObjectList<T>(this List<T> list)
        {
            var result = list.Select(item => (object) item).ToList();
            return result;
        }

        public static List<T> ToSingleElementList<T>(this T item)
        {
            List<T> list = new List<T>();
            list.Add(item);
            return list;
        }

        public static T[] ToSingleElementArray<T>(this T item)
        {
            T[] array = new T[1];
            array[0] = item;
            return array;
        }


        public static bool HasIndex<T>(this List<T> list, int index)
        {
            bool isInScope = index < list.Count;
            return isInScope;
        }

        public static bool IsAnyItem<T>(this List<T> list)
        {
            if (null == list) return false;
            if (list.Count == 0) return false;
            return true;
        }

        public static bool IsAnyItem<T>(this T[] list)
        {
            if (null == list) return false;
            if (list.Length == 0) return false;
            return true;
        }

        public static bool IsNullOrEmpty<T>(this List<T> list)
        {
            return (null == list || list.Count == 0);

        }

        public static bool HasAnyItem<T>(this List<T> list)
        {
            return !IsNullOrEmpty(list);

        }

        public static bool HasAnyItem<T>(this T[] list)
        {
            if (list.IsNullObj()) return false;
            return (list.Length > 0);
        }

        public static bool AddIfNotContains<T>(this ICollection<T> collection, T item)
        {
            if (!collection.Contains(item))
            {
                collection.Add(item);
                return true;
            }

            return false;
        }

        public static bool IsAny<T>(this IEnumerable<T> collection)
        {
            if (null == collection) return false;
            if (collection.Count() == 0) return false;
            return true;
        }

        public static IList<T> ToEmptyIfNull<T>(this IList<T> collection)
        {
            if (collection == null)
                return new List<T>();

            return collection;
        }

        public static bool IsLastIndex<T>(this IList<T> collection, int index)
        {
            if (collection.IsNullObj()) return false;

            return (index == collection.Count - 1);
        }

        public static bool IsFirstIndex<T>(this IList<T> collection, int index)
        {
            if (collection.IsNullObj()) return false;

            return (index == 0);
        }

        public static IList<IList<T>> ToChunks<T>(this IList<T> collection, int chunkSize)
        {
            var chunkCollection = new List<IList<T>>();

            int count = collection.Count;
            if (count <= chunkSize)
            {
                chunkCollection.Add(collection);
                return chunkCollection;
            }

            int repeats = (int) Math.Ceiling(((decimal) count) / ((decimal) chunkSize));
            for (int i = 0; i < repeats; i++)
            {
                var chunk = collection
                    .Skip(i * chunkSize)
                    .Take(chunkSize)
                    .ToList();

                chunkCollection.Add(chunk);
            }

            return chunkCollection;
        }

        public static bool NotContains<T>(this List<T> collection, T item)
        {
            return !collection.Contains(item);
        }

        public static string GetValueOrKey(this Dictionary<string, string> dictionary, string key)
        {
            if (key.IsNullOrEmpty()) return key;
            bool isFound = dictionary.TryGetValue(key, out string value);
            if (isFound)
                return value;
            return key;
        }

        public static TValue GetValueOr<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue valueWhenNoKey)
        {
            if (key.IsNullObj()) return valueWhenNoKey;

            bool isFound = dictionary.TryGetValue(key, out TValue value);
            if (isFound)
                return value;
            return valueWhenNoKey;
        }

        public static T GetRandomItem<T>(this List<T> collection)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int i = rnd.Next(collection.Count);
            return collection[i];
        }

        public static T GetRandomItem<T>(this T[] collection)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int i = rnd.Next(collection.Length);
            return collection[i];
        }
    }
}
