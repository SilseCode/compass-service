using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.BLL.Interfaces;
using Compass.BLL.ViewModels;
using Compass.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;

namespace Compass.BLL.Services
{
    public class AccountService : IAccountService
    {
        public AccountService(UserManager<>)
        {
            
        }
        public Task Register(RegisterViewModel model, string password)
        {
            
        }

        public Task Login(LoginViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
