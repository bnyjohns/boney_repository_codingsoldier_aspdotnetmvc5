using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace CodingSoldier.InfraStructure
{
    public class XMLModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            try
            {
                var modelType = bindingContext.ModelType;
                var serializer = new XmlSerializer(modelType);

                var inputStream = controllerContext.HttpContext.Request.InputStream;
                return serializer.Deserialize(inputStream);
            }
            catch(Exception)
            {
                bindingContext.ModelState.AddModelError("", "The item couldnot be serialized");
                return null;
            }
        }
    }
}