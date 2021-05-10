
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compass.Site.Models;
using CompassSite.Database.Contexts;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using SilseShop.Models;

namespace CompassSite.Services
{
    public class ShopCartManager
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private IServiceProvider _services;
        public ShopCartManager(IProductRepository productRepository, IServiceProvider services, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _services = services;
            _cartRepository = cartRepository;
        }

        public async Task AddToCart(Product product)
        {
            ShopCart cart = GetCart();
            List<ShopCartItem> cartItems = _cartRepository.GetItems(cart.Id).ToList();
            if (cartItems.Any(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id))
            {
                ShopCartItem item = cartItems.FirstOrDefault(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id);
                item.Count++;
                _cartRepository.UpdateCartItem(item);
            }
            else
            {
                ShopCartItem shopCartItem = new ShopCartItem()
                {
                    ShopCart = cart,
                    ShopCartId = cart.Id,
                    Product = product,
                    Count = 1,
                };
                _cartRepository.CreateCartItem(shopCartItem);
            }
            await _cartRepository.SaveChangesAsync();
        }

        public async Task DeleteFromCart(Product product)
        {
            ShopCart cart = GetCart();
            List<ShopCartItem> cartItems = _cartRepository.GetItems(cart.Id).ToList();
            if (cartItems.Any(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id))
            {
                ShopCartItem item = cartItems.FirstOrDefault(i => i.Product.Id == product.Id && i.ShopCartId == cart.Id);
                item.Count--;
                if (item.Count == 0)
                {
                    _cartRepository.RemoveCartItem(item);
                }
                else
                {
                    _cartRepository.UpdateCartItem(item);
                }
                await _cartRepository.SaveChangesAsync();
            }
        }

        public ShopCart GetCart()
        {
            HttpContext httpContext = _services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            if (httpContext.Session.GetString("cart") == null)
            {
                ShopCart cart = new ShopCart();
                string cartId = Guid.NewGuid().ToString();
                cart.Id = cartId;
                cart.DateCreation = DateTime.Now;
                httpContext.Session.SetString("cart", cartId);
                _cartRepository.CreateCart(cart);
                _cartRepository.SaveChangesAsync();
                return cart;
            }
            else
            {
                string cartId = httpContext.Session.GetString("cart");
                ShopCart cart = _cartRepository.GetCart(cartId);
                if (cart == null)
                {
                    cart = new ShopCart();
                    cartId = Guid.NewGuid().ToString();
                    cart.Id = cartId;
                    cart.DateCreation = DateTime.Now;
                    httpContext.Session.SetString("cart", cartId);
                    _cartRepository.CreateCart(cart);
                    _cartRepository.SaveChangesAsync();
                    return cart;
                }
                return cart;
            }
        }
    }
}
