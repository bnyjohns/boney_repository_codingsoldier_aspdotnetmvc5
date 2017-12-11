using AutoMapper;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using CodingSoldier.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CodingSoldier.Controllers
{
    [Authorize]
    public abstract class BaseController<Model, ViewModel> : Controller 
                                                                where Model : class, IModel
                                                                where ViewModel : BaseViewModel, new()
    {
        protected readonly IUnitOfWork _uow;
        protected readonly IRepository<Model> _modelRepository;
        protected readonly IRepository<Category> _categoryRepository;

        public BaseController(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));
            _uow = uow;

            _modelRepository = _uow.Repository<Model>();
            _categoryRepository = _uow.Repository<Category>();
        }        

        [AllowAnonymous]
        public virtual ActionResult Index(int page = 1, int pageSize = 2, string header = null)
        {            
            var modelList = _modelRepository.GetAll().OrderBy(m => m.Id).ToPagedList(page, pageSize);
            var model = new StaticPagedList<ViewModel>(Mapper.Map<IEnumerable<ViewModel>>(modelList), modelList.GetMetaData());
            return View(model);
        }        

        [AllowAnonymous]
        public virtual ActionResult Search(string title)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<SelectListItem> GetAllCategories()
        {
            return _categoryRepository.GetAll().Select(
                                    s => new SelectListItem { Text = s.CategoryName, Value = s.CategoryName });
        }

        public virtual ActionResult Create()
        {
            var viewModel = new ViewModel { Categories = GetAllCategories() };
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(ViewModel viewModel)
        {
            if (viewModel != null)
                viewModel.Categories = GetAllCategories();

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Model>(viewModel);
                _modelRepository.Add(model);
            }
            return View(viewModel);
        }

        public virtual ActionResult Edit(int id)
        {
            var model = _modelRepository.Get(m => m.Id == id);
            var viewModel = Mapper.Map<ViewModel>(model);
            viewModel.Categories = GetAllCategories();
            return View(viewModel);
        }       
        

        [HttpPost]
        public virtual ActionResult Edit(ViewModel viewModel)
        {
            if (viewModel != null)
                viewModel.Categories = GetAllCategories();

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Model>(viewModel);
                _modelRepository.Update(model);
            }
            return View(viewModel);
        }

        public virtual ActionResult Delete(int id)
        {
            _modelRepository.Remove(m => m.Id == id);
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }        
    }
}