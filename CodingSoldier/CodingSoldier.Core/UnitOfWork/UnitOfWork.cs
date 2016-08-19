using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingSoldier.Core.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private CodingSoldierDbContext _dbContext;
        private Dictionary<Type, object> _repositories = new Dictionary<Type, object>();
        public UnitOfWork(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext as CodingSoldierDbContext;
        }

        public IRepository<T> Repository<T>() where T : class
        {
            if(_repositories.Keys.Contains(typeof(T)))
            {
                return _repositories[typeof(T)] as IRepository<T>;
            }
            IRepository<T> repository = new Repository<T>(_dbContext);
            _repositories.Add(typeof(T), repository);
            return repository;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
