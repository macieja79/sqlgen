using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Metaproject
{
    public interface IStringParser<T>
    {
        T Parse(string str);
        bool TryParse(string str, out T result);
    }
}
