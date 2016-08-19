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
    public class CategoryRepository : ICategoryRepository
    {
        private CodingSoldierDbContext _codingSoldierDbContext;
        public CategoryRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _codingSoldierDbContext = dbContext as CodingSoldierDbContext;
        }

        public void Add(Category item)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll(Expression<Func<Category, bool>> predicate = null)
        {
            if (predicate != null)
                _codingSoldierDbContext.Categories.Where(predicate);
            return _codingSoldierDbContext.Categories;
        }

        public Category Get(Expression<Func<Category,bool>> predicate)
        {
            throw new NotImplementedException();
        }        

        public void Update(Category item)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expression<Func<Category, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
