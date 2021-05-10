using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Contexts;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CompassSite.Database.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DatabaseContext _context;

        public ProductRepository(DatabaseContext context)
        {
            _context = context;
        }

        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().Include(t => t.Category).FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _context.Products.AsNoTracking().Include(t => t.Category);
        }

        public IEnumerable<Product> GetByCategory(int categoryId)
        {
            return _context.Products.AsNoTracking().Include(t => t.Category).Where(t => t.CategoryId == categoryId);
        }
        
        public void Update(Product entity)
        {
            _context.Products.Update(entity);
        }

        public void Remove(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

       
    }
}
