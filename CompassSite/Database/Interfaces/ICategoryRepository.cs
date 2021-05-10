using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Models;

namespace CompassSite.Database.Interfaces
{
    public interface ICategoryRepository
    {
        public Category Get(int id);

        public IEnumerable<Category> GetAll();

        public void Update(Category entity);

        public void Remove(Category entity);
        public Task SaveChangesAsync();
    }
}
