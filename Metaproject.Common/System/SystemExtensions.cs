using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class SystemExtensions
    {

        public static string GetNameOf(Expression<Func<object>> exp)
        {
            MemberExpression body = exp.Body as MemberExpression;

            if (body == null)
            {
                UnaryExpression ubody = (UnaryExpression)exp.Body;
                body = ubody.Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public static bool IsNullObj(this object obj)
        {
            return (null == obj);
        }

        public static bool IsNotNullObj(this object obj)
        {
            return null != obj;
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNotNullNorEmpty(this string str)
        {
            return !string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrEmpty(this string[] list)
        {
            return (null == list || list.Length == 0);
        }

        public static bool IsTrue(this bool? value)
        {
            return value.HasValue && value.Value;
        }

        public static bool IsFalse(this bool? value)
        {
            return !value.HasValue || !value.Value;
        }

        public static string ToStrNull(this object obj)
        {
            if (obj == null) return "";
            return obj.ToString();

        }
        public static string Agregate(this string[] obj, string delimiter)
        {
            if (obj == null || obj.Count() == 0)
                return string.Empty;

            return obj.Aggregate((a, b) => a + delimiter + b);
        }

        

        public static bool HasValueAnd<T>(this T? item, Func<T, bool> condition) where T : struct
        {
            if (!item.HasValue) return false;
            bool isTrue = condition(item.Value);
            return isTrue;
        }

        public static bool NotHasValue<T>(this T? item) where T : struct
        {
            return !item.HasValue;
        }

        public static T GetValueOr<T>(this T? item, T valueWhenNull) where T : struct
        {
            if (!item.HasValue) return valueWhenNull;
            return item.Value;
        }

    }
}
