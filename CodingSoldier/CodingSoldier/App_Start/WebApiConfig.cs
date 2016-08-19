using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CodingSoldier
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes(); //Will have preference over the default route below

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.UseXmlSerializer = true; 

            //WebApi by default uses DataContract Serializer
            //So we need to include xmlns(type namespace) with xml data posted, so that it can be deserialized
            //<PostEntity xmlns: i = "http://www.w3.org/2001/XMLSchema-instance" xmlns = "http://schemas.datacontract.org/2004/07/CodingSoldier.Core.Entities" >
            //<CategoryName>ASP.NET</CategoryName >
            //<Id>31</Id>
            //<IsAQuestion>false</IsAQuestion>
            //<PostContent>Bunny</ PostContent>
            //<PostTitle> Bunny Testing</PostTitle>
            //<Tags/>
            //</PostEntity>
            //So here we change webapi to use XmlSerializer instead, so that we can avoid the xmlns attribute
        }
    }
}
