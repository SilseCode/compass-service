using System.ComponentModel.DataAnnotations.Schema;
using CompassSite.Database.Models;

namespace SilseShop.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public string ShopCartId { get; set; }
        [ForeignKey("ShopCartId")]
        public ShopCart ShopCart { get; set; }
    }
}
