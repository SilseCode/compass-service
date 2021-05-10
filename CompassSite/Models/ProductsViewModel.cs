using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompassSite.Database.Models;

namespace Compass.Site.Models
{
    public class ProductsViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
