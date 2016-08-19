using CodingSoldier.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CodingSoldier.Core.Repository
{
    public class PostRepository : IPostRepository
    {
        CodingSoldierDbContext _dbContext;
        public PostRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext as CodingSoldierDbContext;
        }

        public void Add(Post item)
        {
            _dbContext.Posts.Add(item);
            _dbContext.SaveChanges();         
        }

        public IQueryable<Post> GetAll(Expression<Func<Post, bool>> predicate = null)
        {
            if (predicate != null)
                return _dbContext.Posts.Where(predicate);
            return _dbContext.Posts;                      
        }

        public Post Get(Expression<Func<Post,bool>> predicate)
        {
            return _dbContext.Posts.FirstOrDefault(predicate);
        }

        public void Remove(Expression<Func<Post, bool>> predicate)
        {
            var post = Get(predicate);        
            _dbContext.Entry(post).State = EntityState.Deleted;
            _dbContext.SaveChanges();                     
        }

        public void Update(Post item)
        {            
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();                       
        }
    }
}
