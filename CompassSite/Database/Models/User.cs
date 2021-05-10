using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CompassSite.Database.Models
{
    public class User : IdentityUser
    {
        public string CartId { get; set; }
        [ForeignKey("CartId")]
        public ShopCart Cart { get; set; }

    }
    
}
