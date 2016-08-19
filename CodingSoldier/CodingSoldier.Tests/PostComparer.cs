using CodingSoldier.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingSoldier.Tests
{
    internal class PostComparer : IEqualityComparer<Post>
    {
        public bool Equals(Post x, Post y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Post obj)
        {
            return obj.GetHashCode();
        }
    }
}
