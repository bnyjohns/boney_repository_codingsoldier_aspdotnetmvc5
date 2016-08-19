using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingSoldier.Core.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CodingSoldier.Core.Repository
{
    public class StudyRepository : IStudyRepository
    {
        private CodingSoldierDbContext _dbContext;
        public StudyRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext as CodingSoldierDbContext;
        }
        public void Add(Study item)
        {
            _dbContext.Studies.Add(item);
            _dbContext.SaveChanges();
        }

        public Study Get(Expression<Func<Study, bool>> predicate)
        {
            return _dbContext.Studies.FirstOrDefault(predicate);
        }

        public IQueryable<Study> GetAll(Expression<Func<Study,bool>> predicate = null)
        {
            if (predicate != null)
                _dbContext.Studies.Where(predicate);
            return _dbContext.Studies;
        }

        public void Remove(Expression<Func<Study, bool>> predicate)
        {
            var item = Get(predicate);
            _dbContext.Entry(item).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }

        public void Update(Study item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
