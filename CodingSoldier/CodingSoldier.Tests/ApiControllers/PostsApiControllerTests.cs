using AutoMapper;
using CodingSoldier.ApiControllers;
using CodingSoldier.Core.Entities;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.Results;

namespace CodingSoldier.Tests.ApiControllers
{
    [TestClass]
    public class PostsApiControllerTests
    {
        private readonly IRepository<Post> _postRepository;        
        private readonly IUnitOfWork _uow;
        private readonly PostsController _postsApiController;        
        private Post editPost;

        public PostsApiControllerTests()
        {
            TestsHelper.InitializeAutoMapper();
            _uow = Substitute.For<IUnitOfWork>();
            _postRepository = Substitute.For<IRepository<Post>>();         
            _postRepository.GetAll().Returns(TestsHelper.GetInMemoryPosts().AsQueryable());

            editPost = TestsHelper.GetInMemoryPosts().FirstOrDefault(p => p.Id == 6);
            _postRepository.Get(Arg.Any<Expression<Func<Post, bool>>>()).ReturnsForAnyArgs(editPost);

            _uow.Repository<Post>().Returns(_postRepository);
            _postsApiController = new PostsController(_uow);
        }

        [TestMethod]
        public void PostApiController_GetPosts()
        {
            Assert.IsTrue(_postsApiController.GetPosts().Count == 6);
        }

        [TestMethod]
        public void PostApiController_GetPost(int id)
        {
            var result = _postsApiController.GetPost(6) as OkNegotiatedContentResult<PostApiEntity>;
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Content.Id == 6);
        }

        [TestMethod]
        public void PostApiController_PutPost()
        {
            var postApiEntity = new PostApiEntity { Id = 6 };
            var result = _postsApiController.PutPost(postApiEntity) as StatusCodeResult;
            var model = Mapper.Map<Post>(postApiEntity);
            _postRepository.ReceivedWithAnyArgs().Update(model);
            _postRepository.ClearReceivedCalls();            
        }

        [TestMethod]
        public void PostApiController_PostPost()
        {
            var postApiEntity = new PostApiEntity { Id = 6 };
            var result = _postsApiController.PostPost(postApiEntity) as CreatedAtRouteNegotiatedContentResult<Post>;
            var model = Mapper.Map<Post>(postApiEntity);
            _postRepository.ReceivedWithAnyArgs().Add(model);
            _postRepository.ClearReceivedCalls();
        }

        [TestMethod]
        public void PostApiController_DeletePost()
        {
            var postId = 6;
            var result = _postsApiController.DeletePost(postId) as OkNegotiatedContentResult<PostApiEntity>;
            _postRepository.ReceivedWithAnyArgs().Remove(p => p.Id == postId);
            _postRepository.ClearReceivedCalls();
        }
    }
}
