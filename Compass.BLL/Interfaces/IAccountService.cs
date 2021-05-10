using System.Threading.Tasks;
using Compass.BLL.ViewModels;

namespace Compass.BLL.Interfaces
{
    public interface IAccountService
    {
        public Task Register(RegisterViewModel model, string password);
        public Task Login(LoginViewModel model);
        public Task Logout();
    }
}