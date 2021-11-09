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

namespace Restaurant.Context
{
    public class RestaurantContext : IdentityDbContext<User>
    {
        public RestaurantContext(DbContextOptions options) : base(options)
        {
        }


        //public readonly IHttpContextAccessor _httpContextAccessor;

        //public RestaurantContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        //{
        //    _httpContextAccessor = httpContextAccessor;
        //}
        public DbSet<RestInfo> RestInfo { get; set; }
        public DbSet<RestMenu> RestMenu { get; set; }
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
        }

      
    }
}
