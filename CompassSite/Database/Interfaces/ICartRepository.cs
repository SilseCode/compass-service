using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompassSite.Database.Models;
using SilseShop.Models;

namespace CompassSite.Database.Interfaces
{
    public interface ICartRepository
    {
        public Task<ShopCart> GetCart(string id);
        public Task<IEnumerable<ShopCartItem>> GetItems(string shopCartId);
        public Task<ShopCartItem> GetItem(string shopCartId, int itemId);

        public Task UpdateCart(ShopCart shopCart);
        public Task UpdateCartItem(ShopCartItem shopCartItem);
        public Task RemoveCart(ShopCart shopCart);
        public Task RemoveCartItem(ShopCartItem shopCartItem);

        public Task CreateCart(ShopCart shopCart);
        public Task CreateCartItem(ShopCartItem shopCartItem);
        public Task SaveChangesAsync();
    }
}
