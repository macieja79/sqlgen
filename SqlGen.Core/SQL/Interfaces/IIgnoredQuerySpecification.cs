using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlGen
{
    public interface IIgnoredQuerySpecification : ISpecification
    {
        bool IsToIgnore(string sql);
    }
}
