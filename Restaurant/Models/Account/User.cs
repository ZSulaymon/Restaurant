using Microsoft.AspNetCore.Identity;
using Restaurant.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Account
{
    public class User : IdentityUser
    {


        public virtual ICollection<RestInfo> RestInfo { get; set; }
        public virtual ICollection<RestMenu> RestMenus { get; set; }


    }
}
