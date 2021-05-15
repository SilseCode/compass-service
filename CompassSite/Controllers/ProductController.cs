using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compass.Site.Database;
using Compass.Site.Models;
using Compass.Site.Services;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Compass.Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Products(int page = 1)
        {
           
            List<Product> products = _productService.GetProducts();
            ProductsViewModel viewModel = _productService.Paginate(products, page);
            return View(viewModel);
        }
        public async Task<IActionResult> ProductsByCategory(int categoryId, int page = 1)
        {
            List<Product> products = _productService.GetProductsByCategory(categoryId);
            ProductsViewModel viewModel = _productService.Paginate(products, page);
            viewModel.PageInfo.CategoryId = categoryId;
            return View("Products",viewModel);
        }


        public IActionResult GetProductPage(int productId)
        {
            return View("ProductPage");
        }
    }
}
