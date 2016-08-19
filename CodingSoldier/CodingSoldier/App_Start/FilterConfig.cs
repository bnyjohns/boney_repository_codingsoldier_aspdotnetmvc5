using CodingSoldier.Attributes;
using System.Web;
using System.Web.Mvc;

namespace CodingSoldier
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute());
        }
    }
}
