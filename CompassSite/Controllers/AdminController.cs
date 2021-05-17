using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Compass.Site.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController: Controller
    {
        public AdminController()
        {
            
        }
        [HttpGet("/easydata/{**entity}")]
        public async Task<IActionResult> AdminPanel()
        {
            return View();
        }
    }
}
