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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DatabaseContext _context;

        public CategoryRepository(DatabaseContext context)
        {
            _context = context;
        }


        public Category Get(int id)
        {
            return _context.Categories.AsNoTracking().Where(t => t.Id == id).FirstOrDefault();
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.AsNoTracking();
        }

        public void Update(Category entity)
        {
            _context.Categories.Update(entity);
        }

        public void Remove(Category entity)
        {
            _context.Categories.Remove(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
