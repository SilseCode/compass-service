using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Comparers;
using CompassSite.Database.Contexts;
using CompassSite.Database.Models;
using CompassSite.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Compass.Site.Database
{
    public class Initializer
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private bool _initialized;
        public Initializer(DatabaseContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task Init()
        {
            if (!_context.Categories.Any() && !_context.Products.Any())
            {
                List<Product> products = GetProductsFromJson();
                IEnumerable<Category> categories = products.Select(t => t.Category).Distinct(new CategoryComparer());
                await _context.Categories.AddRangeAsync(categories);
                await _context.SaveChangesAsync();
                products.ForEach(t => t.Category = null);
                await _context.Products.AddRangeAsync(products);
                await _context.SaveChangesAsync();
            }
            if (!_context.Users.Any())
            {
                IdentityRole adminRole = new IdentityRole("Admin");
                IdentityRole userRole = new IdentityRole("User");
                await _roleManager.CreateAsync(adminRole);
                await _roleManager.CreateAsync(userRole);

                User admin = new User()
                {
                    Email = "thesilsemail@gmail.com",
                    UserName = "Admin"
                };
                await _userManager.CreateAsync(admin, _configuration["AdminPassword"]);
                await _userManager.AddToRoleAsync(admin, adminRole.Name);
            }
        }

        private List<Product> GetProductsFromJson()
        {
            string jsonData = GetJsonData();
            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonData);
            foreach (Product item in products)
            {
                item.CategoryId = item.Category.Id;
            }
            return products;
        }

        private string GetJsonData()
        {
            string pathToJsonData = string.Format("{0}{1}Database{1}Json{1}data.json", Environment.CurrentDirectory, Path.DirectorySeparatorChar);
            string json = null;
            using (StreamReader sr = new StreamReader(pathToJsonData, Encoding.Default))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
