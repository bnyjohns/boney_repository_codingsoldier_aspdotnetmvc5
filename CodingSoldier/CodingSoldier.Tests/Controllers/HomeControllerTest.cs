using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingSoldier.Controllers;
using CodingSoldier.Core.Entities;

namespace CodingSoldier.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void HomeController_Index()
        {            
            var controller = new HomeController();            
            var result = controller.Index() as ViewResult;            
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void HomeController_About()
        {            
            var controller = new HomeController();            
            var result = controller.About() as ViewResult;            
            Assert.AreEqual(Constants.Me, result.ViewBag.Me);
        }

    }
}
