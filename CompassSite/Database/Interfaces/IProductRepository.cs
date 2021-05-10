using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Models;

namespace CompassSite.Database.Interfaces
{
    public interface IProductRepository
    {
        public Product Get(int id);

        public IEnumerable<Product> GetAll();
        public IEnumerable<Product> GetByCategory(int categoryId);

        public void Update(Product entity);

        public void Remove(Product entity);
        public Task SaveChangesAsync();
    }
}
