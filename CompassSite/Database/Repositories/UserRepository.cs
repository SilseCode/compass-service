using System.Collections.Generic;
using System.Threading.Tasks;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CompassSite.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return _userManager.Users.AsNoTracking();
        }

        public async Task UpdateAsync(User entity)
        {
            await _userManager.UpdateAsync(entity);
        }

        public async Task RemoveAsync(User entity)
        {
            await _userManager.DeleteAsync(entity);
        }

    }
}