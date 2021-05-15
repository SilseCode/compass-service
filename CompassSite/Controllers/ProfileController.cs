using System;
using System.Threading.Tasks;
using Compass.Site.Models;
using CompassSite.Database.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Compass.Site.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public ProfileController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> UpdateProfile([FromForm] ProfileViewModel model, string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            user.AvatarUrl = model.AvatarUrl;
            await _userManager.SetUserNameAsync(user, model.Name);
            await _userManager.UpdateAsync(user);
            var result1 = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
            if (model.NewPassword != null && model.Password != null)
            {
                IdentityResult result = await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Вы ввели неправильный пароль. Пароль не обновлен");
                }
            }

            ProfileViewModel viewModel = new ProfileViewModel()
            {
                AvatarUrl = user.AvatarUrl,
                Name = user.UserName,
            };
            return View("Profile", viewModel);
        }

        public async Task<IActionResult> GetProfile(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel()
                {
                    Name = user.UserName,
                    Password = "",
                    NewPassword = "",
                    AvatarUrl = user.AvatarUrl
                };
                return View("Profile", profileViewModel);
            }

            return View("Profile", new ProfileViewModel());
        }

        public async Task<IActionResult> ChangeAvatar(string userId, string avatarUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            user.AvatarUrl = avatarUrl;
            await _userManager.UpdateAsync(user);
            return View("Profile");
        }
    }
}
