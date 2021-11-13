using Microsoft.Extensions.DependencyInjection;
using System;
using Restaurant.Services.RestInfos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Restaurant.Services.RestMenus;

namespace Restaurant.Services
{
    public static class ServiceExtension
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddAutoMapper(typeof(Startup));
            services.AddScoped<RestInfoService>();
            services.AddScoped<RestMenusService>();
        }
    }
}
