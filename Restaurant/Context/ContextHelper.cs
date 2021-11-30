using Microsoft.AspNetCore.Identity;
using Restaurant.Models.Account;
using Restaurant.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Context
{
    public static class ContextHelper
    {
        public static async Task Seeding(RestaurantContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Where(p => p.NormalizedName.Equals("Admin")).Any())
            {
                var adminRole = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };

                await roleManager.CreateAsync(adminRole);
            }

            if (!userManager.Users.Where(p => p.UserName.Equals("Admin")).Any())
            {
                var adminUser = new User
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com"
                };
                var result = await userManager.CreateAsync(adminUser, "password");

                if (result.Succeeded)
                {
                    var role = await roleManager.FindByNameAsync("Admin");

                    await userManager.AddToRoleAsync(await userManager.FindByNameAsync("Admin"), role.Name);
                }
            }

            if (!context.FoodCategories.Any())
            {
                var foodCategories = new List<FoodCategory>
                {
                    new FoodCategory { Id = Guid.NewGuid(),Name  = "закуски"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "холодное блюдо"},
                    new FoodCategory { Id = Guid.NewGuid(),  Name = "горячие закуски"},
                     new FoodCategory { Id = Guid.NewGuid(), Name = "супы"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "салаты"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "основные блюда"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "стейк"},
                     new FoodCategory { Id = Guid.NewGuid(), Name = "птица / блюда из птицы"},
                    new FoodCategory { Id = Guid.NewGuid(),Name = "рыба и морепродукты"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "гарниры"},
                     new FoodCategory { Id = Guid.NewGuid(),Name = "соусы"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "десерты"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "винная карта"}, 
                };

                context.FoodCategories.AddRange(foodCategories);
                await context.SaveChangesAsync();
                //}
            }

        }
    }
}
