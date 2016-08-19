using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CodingSoldier.InfraStructure
{
    public class ThemeViewEngine : RazorViewEngine
    {
        public ThemeViewEngine(string activeThemeName)
        {
            ViewLocationFormats = new[]
            {
                "~/Views/Themes/" + activeThemeName + "/{1}/{0}.cshtml",
                "~/Views/Themes/" + activeThemeName + "/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
                "",
                ""
            };

            AreaViewLocationFormats = new[]
            {
                "",
                ""
            };       
        }        
    }
}