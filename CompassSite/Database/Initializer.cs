using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Comparers;
using CompassSite.Database.Contexts;
using CompassSite.Database.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Compass.Site.Database
{
    public class Initializer
    {
        private readonly DatabaseContext _context;

        public Initializer(DatabaseContext context)
        {
            _context = context;
        }

        public void Init()
        {
            if(_context.Categories.Count() != 0 || _context.Products.Count() != 0)
            {
                return;
            }
            List<Product> products = GetProductsFromJson();
            IEnumerable<Category> categories = products.Select(t => t.Category).Distinct(new CategoryComparer());
            _context.Categories.AddRange(categories);
            _context.SaveChanges();
            products.ForEach(t=>t.Category = null);
            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        private List<Product> GetProductsFromJson()
        {
            string jsonData = GetJsonData();
            List<Product> products =  JsonConvert.DeserializeObject<List<Product>>(jsonData);
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
