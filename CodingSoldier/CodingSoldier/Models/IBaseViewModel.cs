using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CodingSoldier.Models
{
    public interface IBaseViewModel
    {
        IEnumerable<SelectListItem> Categories { get; set; }
    }
}