using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Restaurant;
using Restaurant.Context;
using Restaurant.Models.Account;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using System;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Program
    {
        //static Mapper InitializeAutomapper()
        //{
        //    var config = new MapperConfiguration(cfg => {
        //        cfg.CreateMap<RestMenu, RestMenusModels>();
        //    });
        //    var mapper = new Mapper(config);
        //    return mapper;
        //}
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                var context = services.GetRequiredService<RestaurantContext>();
                var userManager = services.GetRequiredService<UserManager<User>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.MigrateAsync();

                await ContextHelper.Seeding(context, userManager, roleManager);

                logger.LogInformation("Migrate successfull");
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
