using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compass.Site.Models
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
