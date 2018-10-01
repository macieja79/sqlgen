using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metaproject
{
    public class EnumHelper
    {
        public static int CountExistingFlags(Enum container, Enum[] flags)
        {
            int count = 0;

            foreach (Enum flag in flags)
            {
                if (container.HasFlag(flag))
                {
                    count++;
                }
            }

            return count;
        }

        public static List<string> GetValues(Type enumType)
        {
            var result = new List<string>();
           var array = Enum.GetValues(enumType);
            foreach (var item in array)
            {
                result.Add(item.ToString());
            }

            return result;
        }

    
    }
}
