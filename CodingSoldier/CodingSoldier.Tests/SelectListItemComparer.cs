using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CodingSoldier.Tests
{
    internal class SelectListItemComparer : IEqualityComparer<SelectListItem>
    {
        public bool Equals(SelectListItem x, SelectListItem y)
        {
            return x.Value == y.Value && x.Text == y.Text;
        }

        public int GetHashCode(SelectListItem obj)
        {
            return obj.GetHashCode();
        }
    }
}
