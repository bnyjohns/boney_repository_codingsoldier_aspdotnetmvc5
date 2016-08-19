using System;
using System.Web;
using System.Web.Mvc;

namespace CodingSoldier.InfraStructure
{
    public class XMLModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            var contentType = HttpContext.Current.Request.ContentType.ToLower();
            if(contentType != "text/xml")
            {
                return null;
            }
            return new XMLModelBinder();
        }
    }
}