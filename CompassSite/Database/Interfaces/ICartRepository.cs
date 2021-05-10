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
        public ShopCart GetCart(string id);
        public IEnumerable<ShopCartItem> GetItems(string shopCartId);
        public ShopCartItem GetItem(string shopCartId, int itemId);

        public void UpdateCart(ShopCart shopCart);
        public void UpdateCartItem(ShopCartItem shopCartItem);
        public void RemoveCart(ShopCart shopCart);
        public void RemoveCartItem(ShopCartItem shopCartItem);

        public void CreateCart(ShopCart shopCart);
        public void CreateCartItem(ShopCartItem shopCartItem);
        public Task SaveChangesAsync();
    }
}
