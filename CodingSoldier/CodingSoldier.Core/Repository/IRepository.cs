using System;
using System.Linq;
using System.Linq.Expressions;

namespace CodingSoldier.Core.Repository
{
    public interface IRepository<T>
    {
        void Add(T item);
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        void Remove(Expression<Func<T,bool>> predicate);
        void Update(T item);
        T Get(Expression<Func<T, bool>> predicate);
    }
}
