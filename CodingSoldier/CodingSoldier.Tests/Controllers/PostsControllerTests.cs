using AutoMapper;
using CodingSoldier.Controllers;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using CodingSoldier.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace CodingSoldier.Tests.Controllers
{
    [TestClass]
    public class PostsControllerTests
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _uow;
        private readonly PostsController _postsController;
        private Post editPost;    

        public PostsControllerTests()
        {
            TestsHelper.InitializeAutoMapper();
            _uow = Substitute.For<IUnitOfWork>();
            _postRepository = Substitute.For<IRepository<Post>>();
            _categoryRepository = Substitute.For<IRepository<Category>>();
            _categoryRepository.GetAll().Returns(TestsHelper.GetInMemoryCagegories().AsQueryable());
            _postRepository.GetAll().Returns(TestsHelper.GetInMemoryPosts().AsQueryable());

            editPost = TestsHelper.GetInMemoryPosts().FirstOrDefault(p => p.Id == 6);
            _postRepository.Get(Arg.Any<Expression<Func<Post, bool>>>()).ReturnsForAnyArgs(editPost);

            _uow.Repository<Post>().Returns(_postRepository);
            _uow.Repository<Category>().Returns(_categoryRepository);           

            _postsController = new PostsController(_uow);
        } 

        [TestMethod]
        public void PostsController_Index()
        {
            var viewResult = _postsController.Index(1, 2) as ViewResult;
            Assert.IsNotNull(viewResult);
            var model = viewResult.Model as IEnumerable<PostViewModel>;
            Assert.IsNotNull(model);
            Assert.IsTrue(model.Count() == 2);
            Assert.IsTrue(model.First().Id == 1);            
        }

        [TestMethod]
        public void PostsController_Create_Get()
        {
            var viewResult = _postsController.Create() as ViewResult;
            Assert.IsTrue(viewResult != null);
            var categories = (viewResult.Model as PostViewModel).Categories.ToList();
            Assert.IsTrue(categories.SequenceEqual(TestsHelper.GetCategories(), new SelectListItemComparer()));
        }

        [TestMethod]
        public void PostsController_Create_Post()
        {
            //Here post is actually Invalid. But model state will be true since the model is not passed via MVC pipeline.
            //Testing validation would actually be testing MVC Model Validation and hence skipping that part
            var postViewModel = new PostViewModel { Id = 6 };
            var viewResult = _postsController.Create(postViewModel) as ViewResult;
            var model = Mapper.Map<Post>(postViewModel);
            _postRepository.ReceivedWithAnyArgs().Add(model);
            _postRepository.ClearReceivedCalls();

            var categories = (viewResult.Model as PostViewModel).Categories.ToList();
            Assert.IsTrue(categories.SequenceEqual(TestsHelper.GetCategories(), new SelectListItemComparer()));
        }

        [TestMethod]
        public void PostsController_Edit_Get()
        {
            var postId = 6;
            var viewResult = _postsController.Edit(postId) as ViewResult;
            var categories = (viewResult.Model as PostViewModel).Categories.ToList();
            Assert.IsTrue(categories.SequenceEqual(TestsHelper.GetCategories(), new SelectListItemComparer()));
        }

        [TestMethod]
        public void PostsController_Edit_Post()
        {
            var postViewModel = new PostViewModel{ Id = 6 };
            var viewResult = _postsController.Edit(postViewModel) as ViewResult;
            var model = Mapper.Map<Post>(postViewModel);
            _postRepository.ReceivedWithAnyArgs().Update(model);
            _postRepository.ClearReceivedCalls();

            var categories = (viewResult.Model as PostViewModel).Categories;
            Assert.IsTrue(categories.SequenceEqual(TestsHelper.GetCategories(), new SelectListItemComparer()));
        }

        [TestMethod]
        public void PostsController_Delete()
        {
            var postId = 6;
            var viewResult = _postsController.Delete(postId) as ViewResult;
            _postRepository.ReceivedWithAnyArgs().Remove(p => p.Id == postId);
            _postRepository.ClearReceivedCalls();
        }
    }
}
