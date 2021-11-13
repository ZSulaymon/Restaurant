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
        //Create class Rest
        //public virtual ICollection<Restaurant>  Restaurants { get; set; }
        //public int DzoID
        //{
        //    get
        //    {
        //        var UserManager = new UserManager<ApplicationUser>
        //            (new UserStore<ApplicationUser>(new ApplicationDbContext()));

        //        return UserManager.FindById(User.Identity.GetUserId()).DzoID;
        //    }
        //}
        //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

        public virtual ICollection<RestInfo> RestInfo { get; set; }
        //public virtual ICollection<RestMenu> RestMenus { get; set; }


    }
}
