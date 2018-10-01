using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Metaproject
{
    public static class EnumExtensions
    {
        public static bool IsIn(this System.Enum item, params System.Enum[] items)
        {
            return items.Any(i => i.Equals(item));
        }

        
        

       

    }
}
