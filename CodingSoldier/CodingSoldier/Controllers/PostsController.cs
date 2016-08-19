using CodingSoldier.Core.Models;
using CodingSoldier.Core.UnitOfWork;
using CodingSoldier.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace CodingSoldier.Controllers
{
    public class PostsController : BaseController<Post, PostViewModel>
    {
        public PostsController(IUnitOfWork uow) : base(uow)
        {            

        }        
    }
}