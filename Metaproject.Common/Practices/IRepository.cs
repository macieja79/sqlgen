using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Metaproject
{
    public interface IRepository<T>
    {
        T Get(Guid id);
        IEnumerable<T> Find(Expression<Func<T, bool>> pred);
        IEnumerable<T> Find(Func<T, bool> pred);
        IEnumerable<T> GetAll();
        void Add(T item);
        void Remove(T item);
    }
}
