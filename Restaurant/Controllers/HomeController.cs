using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Context;
using Restaurant.Models;
using Restaurant.Models.Restaurant;
using Restaurant.Services.RestInfos;
using Restaurant.Services.RestMenus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Restaurant.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly RestaurantContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly RestInfoService _restInfoService;
        private readonly RestMenusService _restMenusService;
        private List<ShopCartItem> listOfshopingCartModels;


        public HomeController(ILogger<HomeController> logger,
            RestInfoService restInfoService,
            RestMenusService restMenusService,
            RestaurantContext context)
        {
            _context = context;
            _logger = logger;
            _restInfoService = restInfoService;
            _restMenusService = restMenusService;
            listOfshopingCartModels = new List<ShopCartItem>();
        }

        public async Task<IActionResult> Index()
        {
            var allRest = await _restInfoService.GetAll();
            return (IActionResult)View(allRest);
        }

        // GET: RestMenus1
         public async Task<IActionResult> GetMenu(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restMenuById = await _restMenusService.GetMenuById(id);
            return View(restMenuById);

        }

        private IActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        //public async Task<IActionResult> Create(RestInfo model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }
        //    string finalFileName = null;
        //    if (model.ImageFile != null)
        //    {
        //        finalFileName = await CopyFile(model.ImageFile);
        //    }
        //    var currenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    // var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //    var restInfo = new RestInfo
        //    {
        //        RestName = model.RestName,
        //        RestAdministrator = model.RestAdministrator,
        //        RestPhone = model.RestPhone,
        //        ImageName = finalFileName,
        //        InsertDateTime = DateTime.Now,
        //        UserId = currenUserId,
        //        RestAddress = model.RestAddress,
        //        RestReferencePoint = model.RestReferencePoint,
        //        Description = model.Description,
        //        UpdateDate = null

        //    };
        //    _context.Add(restInfo);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
        public async Task<Microsoft.AspNetCore.Mvc.JsonResult>  GetMenuId(Guid ItemId)
        {
            //if (ItemId == null)
            //{
            //    return View(GetMenu);
            //}
            if (ItemId == null)
            {
                return (Microsoft.AspNetCore.Mvc.JsonResult)NotFound();
            }
            var restMenuById = await _restMenusService.GetMenuById(ItemId);
            //return View(restMenuById);

            return Json("", JsonRequestBehavior.AllowGet);
        }

      

        public IActionResult Privacy()
        {
            return (IActionResult)View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(model: new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
