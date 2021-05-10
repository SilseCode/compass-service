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

namespace Compass.Site.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductRepository _productRepository;
        private ShopCartManager _cartManager;
        private readonly ICartRepository _cartRepository;
        public CartController(ProductRepository productRepositoryRepository, ShopCartManager cartManager, ICartRepository cartRepository)
        {
            _productRepository = productRepositoryRepository;
            _cartManager = cartManager;
            _cartRepository = cartRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int id)
        {
            await _cartManager.AddToCart(_productRepository.Get(id));
            return RedirectToAction("ShopCart", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFromCart(int id)
        {
            await _cartManager.DeleteFromCart(_productRepository.Get(id));
            return RedirectToAction("ShopCart", "Cart");
        }

        [HttpGet]
        public IActionResult ShopCart()
        {
            ShopCart cart = _cartManager.GetCart();
            //var shopCartItems = _cartRepository.GetItems(cart.Id).Select(t=> new ProductsViewModel(t.Product));
            //return View(shopCartItems);
            throw new NotImplementedException();
        }

        [HttpGet]
        public IActionResult GetProduct(int id)
        {
            Product product = _productRepository.Get(id);
            return new ObjectResult(product);
        }
    }
}
