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
                    new FoodCategory { Id = Guid.NewGuid(),Name  = "appetizers "},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "cold platter"},
                    new FoodCategory { Id = Guid.NewGuid(),  Name = "hot appetizers"},
                    new FoodCategory { Id = Guid.NewGuid(),  Name = "startes"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "soups"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "salads"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "main dishes"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "meat"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "steak "},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "poultry / poultry dishes"},
                    new FoodCategory { Id = Guid.NewGuid(),Name = "fish and seafoods"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "side dishes / sides"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "snacks"},
                    new FoodCategory { Id = Guid.NewGuid(),Name = "souces"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "desserts"},
                    new FoodCategory { Id = Guid.NewGuid(), Name = "wine list"}, 
                };

                context.FoodCategories.AddRange(foodCategories);
                await context.SaveChangesAsync();
                //}
            }

        }
    }
}
