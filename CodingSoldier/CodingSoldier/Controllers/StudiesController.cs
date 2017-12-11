using System.Web.Mvc;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.UnitOfWork;
using CodingSoldier.Models;
using CodingSoldier.Core.Repository;
using System.Linq;
using PagedList;
using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CodingSoldier.Controllers
{
    public class StudiesController : BaseController<Study, StudyViewModel>
    {
        public StudiesController(IUnitOfWork uow) :
            base(uow)
        {

        }

        public override ActionResult Index(int page = 1, int pageSize = 2, string studyHeader = null)
        {
            if (studyHeader != null)
            {
                var model = _modelRepository.GetAll().Where(m => m.StudyHeader == studyHeader);
                var viewModel = Mapper.Map<IEnumerable<StudyViewModel>>(model);
                return View(viewModel);
            }
            else
            {
                return base.Index(page, pageSize);
            }      
        }

        public override ActionResult Search(string title)
        {
            ViewBag.SearchText = title;
            var studies = _modelRepository as IRepository<Study>;
            var searchedStudies = studies.GetAll().Where(s => s.StudyHeader.Contains(title) || s.StudyContent.Contains(title));
            var viewModel = Mapper.Map<List<StudyViewModel>>(searchedStudies);            
            return View(viewModel);
        }
    }
}