using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compass.Site.Models;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using CompassSite.Database.Repositories;
using CompassSite.Services;
using Microsoft.AspNetCore.Mvc;
using SilseShop.Models;

namespace Compass.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private ShopCartManager _cartManager;
        private readonly ICartRepository _cartRepository;
        public CartController(IProductRepository productRepository, ShopCartManager cartManager, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartManager = cartManager;
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            Product product = _productRepository.Get(id);
            await _cartManager.AddToCart(product);
            return RedirectToAction("ShopCart", "Cart");
        }

        [HttpPost()]
        public async Task<IActionResult> DeleteFromCart(int id)
        {
            await _cartManager.DeleteFromCart(_productRepository.Get(id));
            return RedirectToAction("ShopCart", "Cart");
        }

        [HttpGet]
        public async Task<IActionResult> ShopCart()
        {
            ShopCart cart = await _cartManager.GetCart();
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            Product product = _productRepository.Get(id);
            return new ObjectResult(product);
        }

    }
}
