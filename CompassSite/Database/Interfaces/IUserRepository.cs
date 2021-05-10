using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Repositories;
using CompassSite.Database.Models;
using Microsoft.AspNetCore.Identity;

namespace CompassSite.Database.Interfaces
{
    public interface IUserRepository
    {
        public Task<User> GetAsync(string id);

        public Task<IEnumerable<User>> GetAllAsync();

        public Task UpdateAsync(User entity);

        public Task RemoveAsync(User entity);
       
    }
}
