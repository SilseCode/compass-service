using System;
using System.Collections.Generic;
using System.Text;
using CompassSite.Database.Models;

namespace CompassSite.Database.Comparers
{
    public class CategoryComparer : IEqualityComparer<Category>
    {
        public bool Equals(Category x, Category y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id;
        }

        public int GetHashCode(Category obj)
        {
            return obj.Id;
        }
    }
}
