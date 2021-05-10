using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Contexts;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;
using Microsoft.EntityFrameworkCore;
using SilseShop.Models;

namespace CompassSite.Database.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly DatabaseContext _context;

        public CartRepository(DatabaseContext context)
        {
            _context = context;
        }

        public ShopCart GetCart(string id)
        {
           return _context.Carts.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<ShopCartItem> GetItems(string shopCartId)
        {
            return _context.CartItems.Where(t => t.ShopCartId == shopCartId);
        }

        public ShopCartItem GetItem(string shopCartId, int itemId)
        {
            return _context.CartItems.FirstOrDefault(t => t.ShopCartId == shopCartId && t.Id == itemId);
        }

        public void UpdateCart(ShopCart shopCart)
        {
             _context.Carts.Update(shopCart);
        }

        public void UpdateCartItem(ShopCartItem shopCartItem)
        {
            _context.CartItems.Update(shopCartItem);
        }

        public void RemoveCart(ShopCart shopCart)
        {
            _context.Carts.Remove(shopCart);
        }

        public void RemoveCartItem(ShopCartItem shopCartItem)
        {
            _context.CartItems.Remove(shopCartItem);
        }

        public void CreateCart(ShopCart shopCart)
        {
            _context.Carts.Add(shopCart);
        }

        public void CreateCartItem(ShopCartItem shopCartItem)
        {
            _context.CartItems.Add(shopCartItem);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
