using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SilseShop.Models;

namespace CompassSite.Database.Models
{
    public class ShopCart
    {
        public string Id { get; set; }
        public DateTime DateCreation { get; set; }
        public ICollection<ShopCartItem> ShopCartItems { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
