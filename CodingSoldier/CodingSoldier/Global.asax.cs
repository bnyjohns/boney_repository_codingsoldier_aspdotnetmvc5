using CodingSoldier.InfraStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CodingSoldier
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ViewEngines.Engines.Insert(0, new ThemeViewEngine()); To insert a new ViewEngine            
            //ModelBinderProviders.BinderProviders.Insert(0, new XMLModelBinderProvider()); //To add a custom modelbinderprovider
            LogConfig.ConfigureLogs();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.ConfigureAutoMapper();            
        }
    }
}
