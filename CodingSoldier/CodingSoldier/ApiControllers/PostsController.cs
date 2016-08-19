using AutoMapper;
using CodingSoldier.Core.Entities;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.UnitOfWork;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace CodingSoldier.ApiControllers
{
    [RoutePrefix("api")]
    public class PostsController : BaseApiController<Post, PostApiEntity>
    {
        public PostsController(IUnitOfWork uow) : base(uow)
        {
           
        }

        // GET: api/Posts
        [AllowAnonymous]
        public List<PostApiEntity> GetPosts()
        {            
            return GetAll();
        }

        // GET: api/Posts/5
        [AllowAnonymous]
        [ResponseType(typeof(PostApiEntity))]
        public IHttpActionResult GetPost(int id)
        {
            return Get(id);
        }

       
        [ResponseType(typeof(void))]
        [Route("updatepost")] 
        [HttpPost]
        public IHttpActionResult PutPost(PostApiEntity post)
        {
            var model = Mapper.Map<Post>(post);
            return UpdateModel(model);
        }

        // POST: api/Posts        
        [ResponseType(typeof(PostApiEntity))]
        [Route("addpost")]//This will override the default route in webapi. Or to rephrase "/api/Posts/" wont work.
        public IHttpActionResult PostPost(PostApiEntity post)
        {
            var model = Mapper.Map<Post>(post);
            return PostModel(model);
        }

        // DELETE: api/Posts/5
        [ResponseType(typeof(PostApiEntity))]
        public IHttpActionResult DeletePost(int id)
        {
            return DeleteModel(id);
        }        
    }
}
