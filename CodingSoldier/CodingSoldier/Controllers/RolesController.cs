using CodingSoldier.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingSoldier.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        ApplicationDbContext _dbContext;

        public RolesController(IUnityContainer container)
        {
            _dbContext = container.Resolve<DbContext>("ApplicationDbContext") as ApplicationDbContext;
        }

        public ActionResult Index()
        {
            var Roles = _dbContext.Roles.ToList();
            return View(Roles);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            var role = new IdentityRole();
            return View(role);
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {
            _dbContext.Roles.Add(role);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}