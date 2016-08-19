using System.Collections.Generic;
using System.Web.Mvc;

namespace CodingSoldier.Models
{
    public abstract class BaseViewModel
    {
        public virtual IEnumerable<SelectListItem> Categories { get; set; }        
    }
}