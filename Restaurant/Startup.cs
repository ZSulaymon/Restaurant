using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Restaurant.Context;
using Restaurant.Models.Account;
using System;
using Microsoft.AspNetCore.Http;
using Restaurant.Services;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Interfaces;
using Restaurant.Controllers;

namespace Restaurant
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(Startup));
            services.InitServices();
            services.AddControllersWithViews();
            //add interfaces
            services.AddTransient<IAllOrders, OrdersRepository>();
            services.AddTransient<HomeController>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShopCart.GetCart(sp));
            services.AddMvc(options => options.MaxModelValidationErrors = 50); //максимального количества волидации. по умолчанию это 200 ошибок

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            
           


            services.AddDbContext<RestaurantContext>(option =>
            {
                option.UseSqlServer(Configuration.GetConnectionString("Default")).UseLazyLoadingProxies();
            });

            services.AddIdentity<User, IdentityRole>(option =>
            {
                option.User.AllowedUserNameCharacters = null;
            }).AddRoleManager<RoleManager<IdentityRole>>()
              .AddUserManager<UserManager<User>>()
              .AddEntityFrameworkStores<RestaurantContext>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.AddAuthorization();

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireLowercase = false;
                option.Password.RequireUppercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredLength = 1;
                option.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(option =>
            {
                option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                option.SlidingExpiration = true;
            });

            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.AddSingleton<IRestMenusService, RestMenusService>();
            //services.AddAutoMapper(ServiceProvider.MapperExpression) =>
            //        MapperExpression.ConstructServicesUsing(t => serviceProvider);
            //MapperExpression.CreateMap(typeof(), typeof(UserViewModel));

            //);

 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            
        }
    }
}
