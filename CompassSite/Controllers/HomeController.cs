using CompassSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compass.Site.Database;
using Compass.Site.Models;
using Compass.Site.Services;
using CompassSite.Database.Contexts;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CompassSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;

        public HomeController(ILogger<HomeController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        [HttpGet("/")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ViewResult> Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet("about")]
        public async Task<IActionResult> About()
        {
            return View();
        }
        [HttpGet("contacts")]
        public IActionResult Contacts()
        {
            return View();
        }
        [HttpGet("services")]
        public async Task<IActionResult> Services()
        {
            return View();
        }

    }
}
