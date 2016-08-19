using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using CodingSoldier.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;

namespace CodingSoldier.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IUnitOfWork _uow;
        public PostsController(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));

            _uow = uow;
            _postRepository = uow.Repository<Post>();
            _categoryRepository = uow.Repository<Category>();
        }        

        //
        // GET: /Posts/
        [AllowAnonymous]
        public ActionResult Index(int page = 1, int pageSize = 2)
        {            
            var posts = _postRepository.GetAll().OrderBy(p => p.PostId).ToPagedList(page, pageSize);
            //var model = new StaticPagedList<PostViewModel>(posts.ConvertTo<PostViewModel, Post>(), posts.GetMetaData());       
            var model = new StaticPagedList<PostViewModel>(Mapper.Map<IEnumerable<PostViewModel>>(posts), posts.GetMetaData());
            return View(model);
        }        

        public ActionResult Create()
        {
            var model = new PostViewModel { Categories = GetAllCategories() };               
            return View(model);
        }

        private IEnumerable<SelectListItem> GetAllCategories()
        {
            return _categoryRepository.GetAll().Select(
                                    s => new SelectListItem { Text = s.CategoryName, Value = s.CategoryName });
        }

        [HttpPost]
        public ActionResult Create(PostViewModel post)
        {
            if (post != null)
                post.Categories = GetAllCategories();

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Post>(post);
                _postRepository.Add(model);                
            }
            return View(post);
        }

        public ActionResult Edit(int postId)
        {            
            var model = _postRepository.Get(p => p.PostId == postId);
            var viewModel = Mapper.Map<PostViewModel>(model);
            viewModel.Categories = GetAllCategories();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(PostViewModel post)
        {
            if (post != null)
                post.Categories = GetAllCategories();

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Post>(post);
                _postRepository.Update(model);                
            }
            return View(post);
        }

        public ActionResult Delete(int postId)
        {
            _postRepository.Remove(p => p.PostId == postId);            
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }
    }
}