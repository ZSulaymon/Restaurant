using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections;
using Restaurant.Models.Restaurant.ViewModels;

namespace Restaurant.Context
{
    public class RestaurantContext : IdentityDbContext<User>
    {
        public RestaurantContext(DbContextOptions options) : base(options)
        {
        }
 
        public DbSet<RestInfo> RestInfo { get; set; }
        public DbSet<RestMenu> RestMenus { get; set; }
        public DbSet<FoodCategory> FoodCategories { get; set; }
        //public IEnumerable RestInfoModels { get; internal set; }
        //public IEnumerable RestMenusModels { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity => entity.ToTable("Users"));
            builder.Entity<IdentityRole>(entity => entity.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
            builder.Entity<RestInfo>(entity => entity.ToTable("RestInfo"));
            builder.Entity<RestMenu>(entity => entity.ToTable("RestMenu"));
            builder.Entity<FoodCategory>(entity => entity.ToTable("FoodCatigories"));
        }
        //public IEnumerable RestInfoModels { get; internal set; }
        //public IEnumerable RestMenusModels { get; internal set; }

       // public DbSet<Restaurant.Models.Restaurant.ViewModels.RestInfoModels> RestInfoModels { get; set; }
        //public DbSet<Restaurant.Models.Restaurant.ViewModels.RestMenusModels> RestMenusModels { get; set; }


    }
}
