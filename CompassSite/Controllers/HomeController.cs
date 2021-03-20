using CompassSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CompassSite.Controllers
{
    using System.Diagnostics.CodeAnalysis;

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ViewResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> About()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        public async Task<IActionResult> Services()
        {
            return View();
        }

        public async Task<IActionResult> Products()
        {
            throw new NotImplementedException();
        }
    }
}
