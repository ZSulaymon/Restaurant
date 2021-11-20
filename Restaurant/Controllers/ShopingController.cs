using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Context;
using Restaurant.Services.RestMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class ShopingController : Controller
    {
        private readonly RestaurantContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RestMenusService _restMenusService;


        public ShopingController(RestaurantContext context,
            IWebHostEnvironment webHostEnvironment,
            RestMenusService restMenusService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _restMenusService = restMenusService;

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
