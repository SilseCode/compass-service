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

        public async Task<ShopCart> GetCart(string id)
        {
            return await _context.Carts.AsNoTracking()
               .Include(t => t.ShopCartItems).ThenInclude(t=>t.Product).ThenInclude(t=>t.Category)
               .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<ShopCartItem>> GetItems(string shopCartId)
        {
            return await _context.CartItems.AsNoTracking()
               .Include(t => t.Product)
               .Include(t=>t.Product.Category)
               .Include(t => t.ShopCart)
               .Where(t => t.ShopCartId == shopCartId).ToListAsync();
        }

        public async Task<ShopCartItem> GetItem(string shopCartId, int itemId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(t => t.ShopCartId == shopCartId && t.Id == itemId);
        }

        public async Task UpdateCart(ShopCart shopCart)
        {
             _context.Carts.Update(shopCart);
        }

        public async Task UpdateCartItem(ShopCartItem shopCartItem)
        {
            _context.CartItems.Update(shopCartItem);
        }

        public async Task RemoveCart(ShopCart shopCart)
        {
            _context.Carts.Remove(shopCart);
        }

        public async Task RemoveCartItem(ShopCartItem shopCartItem)
        {
            _context.CartItems.Remove(shopCartItem);
        }

        public async Task CreateCart(ShopCart shopCart)
        {
            await _context.Carts.AddAsync(shopCart);
        }

        public async Task CreateCartItem(ShopCartItem shopCartItem)
        {
            await _context.CartItems.AddAsync(shopCartItem);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
