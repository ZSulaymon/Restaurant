using Microsoft.Extensions.DependencyInjection;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public static class ServiceExtension
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //services.AddScoped<RestInfoService>();
        }
    }
}
