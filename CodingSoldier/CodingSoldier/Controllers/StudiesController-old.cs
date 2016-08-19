using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using CodingSoldier.Models;
using AutoMapper;

namespace CodingSoldier.Controllers
{
    [Authorize(Roles ="Admin")]
    public class StudiesController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Study> _studyRepository;
        private readonly IRepository<Category> _categoryRepository;
        public StudiesController(IUnitOfWork uow)
        {
            if (uow == null)
                throw new ArgumentNullException(nameof(uow));
            _uow = uow;

            _studyRepository = _uow.Repository<Study>();
            _categoryRepository = _uow.Repository<Category>();
        }
        
        // GET: Study
        public ActionResult Index(int page = 1, int pageSize = 2)
        {            
            var studies = _studyRepository.GetAll().OrderBy(s => s.StudyId).ToPagedList(page, pageSize);
            var model = new StaticPagedList<StudyViewModel>(Mapper.Map<IEnumerable<StudyViewModel>>(studies), studies.GetMetaData());
            return View(model);
        }

        private IEnumerable<SelectListItem> GetAllCategories()
        {
            return _categoryRepository.GetAll().Select(
                                    s => new SelectListItem { Text = s.CategoryName, Value = s.CategoryName });
        }

        public ActionResult Create()
        {
            var viewModel = new StudyViewModel { Categories = GetAllCategories() };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(StudyViewModel study)
        {
            if (study != null)
                study.Categories = GetAllCategories();

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Study>(study);
                _studyRepository.Add(model);
            }
            return View(study);
        }

        public ActionResult Edit(int studyId)
        {            
            var model = _studyRepository.Get(s => s.StudyId == studyId);
            var viewModel = Mapper.Map<StudyViewModel>(model);
            viewModel.Categories = GetAllCategories();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(StudyViewModel study)
        {
            if (study != null)
                study.Categories = GetAllCategories();

            if (ModelState.IsValid)
            {
                var model = Mapper.Map<Study>(study);
                _studyRepository.Update(model);
            }
            return View(study);
        }

        public ActionResult Delete(int studyId)
        {
            _studyRepository.Remove(s => s.StudyId == studyId);
            return RedirectToAction("index");
        }

        protected override void Dispose(bool disposing)
        {
            _uow.Dispose();
            base.Dispose(disposing);
        }
        
    }
}