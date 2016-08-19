using CodingSoldier.Core.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace CodingSoldier.Core.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        CodingSoldierDbContext _dbContext;
        public Repository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext as CodingSoldierDbContext;
        }

        public void Add(T item)
        {
            _dbContext.Set<T>().Add(item);
            _dbContext.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbContext.Set<T>().FirstOrDefault(predicate);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            var items = _dbContext.Set<T>();
            if (predicate != null)
                return items.Where(predicate);
            return items;
        }

        public void Remove(Expression<Func<T, bool>> predicate)
        {
            var item = Get(predicate);
            _dbContext.Entry(item).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public void Update(T item)
        {
            _dbContext.Entry<T>(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
