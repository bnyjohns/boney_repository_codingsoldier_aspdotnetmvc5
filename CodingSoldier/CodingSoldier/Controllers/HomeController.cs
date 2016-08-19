using CodingSoldier.Core.Entities;
using CodingSoldier.Core.Models;
using CodingSoldier.Core.Repository;
using CodingSoldier.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingSoldier.Controllers
{
    public class HomeController : Controller
    {               
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult About()
        {            
            ViewBag.Me = Constants.Me;
            return View();
        }

        public ActionResult Technologies()
        {
            var technologies = @"MVC5, EF6, WEBAPI2, Unity for both MVC and WEBAPI, AutoMapper, CustomErrorFilter,
                                 Log4Net, PagedList, WebAPI Help Page, EF Identity, BootStrap, Attribute Based Routing,
                                 Owin for Authentication,";

            var patternsAndPractices = @"UnitOfWork Pattern, Repository Pattern,  CodeFirst Migration(later changed to DB First),
                            BaseControllers using Generics, Unit Tests for Controlles and Repositories";

            ViewBag.technologies = technologies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            ViewBag.patternsAndPractices = patternsAndPractices.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            return View();
         }
    }
}