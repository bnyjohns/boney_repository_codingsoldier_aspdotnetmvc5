using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System.Data.Entity;
using System.Linq;

namespace CodingSoldier.Tests.Repositories
{
    [TestClass]
    public class RepositoryTests
    {
        private CodingSoldierDbContext _codingSoldierDbContext;
        private IRepository<Post> _postRepository;
        public RepositoryTests()
        {
            var data = GetPosts();
            var posts = Substitute.For<DbSet<Post>, IQueryable<Post>>();
            (posts as IQueryable).Provider.Returns(data.Provider);
            (posts as IQueryable).Expression.Returns(data.Expression);
            (posts as IQueryable).ElementType.Returns(data.ElementType);
            (posts as IQueryable).GetEnumerator().Returns(data.GetEnumerator());

            Database.SetInitializer<CodingSoldierDbContext>(null);
            _codingSoldierDbContext = Substitute.For<CodingSoldierDbContext>();
            _codingSoldierDbContext.Set<Post>().Returns(posts);

            _postRepository = new Repository<Post>(_codingSoldierDbContext);
        }

        private IQueryable<Post> GetPosts()
        {      
            return TestsHelper.GetInMemoryPosts().AsQueryable();
        }

        [TestMethod]
        public void Repository_Add()
        {
            var post = new Post { Id = 6 };
            _postRepository.Add(post);
            _codingSoldierDbContext.Set<Post>().Received().Add(post);
            _codingSoldierDbContext.Received().SaveChanges();
        }

        [TestMethod]
        public void Repository_GetAll()
        {
            var actual = _postRepository.GetAll();
            var expected = GetPosts();            
            Assert.IsTrue(actual.SequenceEqual(expected, new PostComparer()));
        }

        [TestMethod]
        public void Repository_Get()
        {
            var Id = 1;
            var post = _postRepository.Get(p => p.Id == Id);
            Assert.IsTrue(post.Id == GetPosts().First(p => p.Id == 1).Id);
        }

        [TestMethod]
        public void Repository_Remove()
        {
            var Id = 1;
            var post = new Post { Id = Id };
            _postRepository.Remove(p => p.Id == Id);
            _codingSoldierDbContext.ReceivedWithAnyArgs().Entry<Post>(post);
            _codingSoldierDbContext.Received().SaveChanges();
            
        }

        [TestMethod]
        public void Repository_Update()
        {
            var post = new Post { Id = 6 };
            _postRepository.Update(post);
            _codingSoldierDbContext.Received().Entry(post);
            _codingSoldierDbContext.Received().SaveChanges();
        }
    }
}
