using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Models;
using Restaurant.Services.RestInfos;
using Restaurant.Services.RestMenus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RestInfoService _restInfoService;
        private readonly RestMenusService _restMenusService;


        public HomeController(ILogger<HomeController> logger, RestInfoService restInfoService, RestMenusService restMenusService )
        {
            _logger = logger;
            _restInfoService = restInfoService;
            _restMenusService = restMenusService;
        }

        public async Task<IActionResult> Index()
        {
            var allRest = await _restInfoService.GetAll();
            return View(allRest);
        }

        // GET: RestMenus1
        public async Task<IActionResult> GetManu(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restMenuById = await _restMenusService.GetMenuById(id);
            return View(restMenuById);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
