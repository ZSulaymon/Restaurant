using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Restaurant.Services.RestMenus;

namespace Restaurant.Controllers
{
    public class RestMenusController : Controller
    {
        //private readonly RestaurantContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
       // private readonly RestMenusService _restMenusService;
        //public RestMenusController(RestaurantContext context,
        //    IWebHostEnvironment webHostEnvironment, RestMenusService restInfoService)
        //{
        //    _context = context;
        //    _webHostEnvironment = webHostEnvironment;
        //    _restMenusService = restInfoService;
        //}
        private readonly RestaurantContext _context;
        public RestMenusController(RestaurantContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
          //  _restMenusService = restMenusService;
        }
        // GET: RestMenus
        public async Task<IActionResult> Index()
        {
            var restaurantContext = _context.RestMenus.Include(r => r.RestInfo);
            return View(await restaurantContext.ToListAsync());
        }
        //public async Task<IActionResult> Index()
        //{
        //    //var dateString = DateTime.Now.ToStringTajikFormat(DateFormats.Russian);

        //    var movies = await _restMenusService.GetAll();
        //    return View(movies);
        //}
        // GET: RestMenus/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restMenu = await _context.RestMenus
                .Include(r => r.RestInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restMenu == null)
            {
                return NotFound();
            }
            return View(restMenu);
        }
        [NonAction]
        private async Task<string> CopyFile(IFormFile imageFile)
        {
            if (imageFile == null) return null;
            var rootPath = _webHostEnvironment.WebRootPath;
            var filename = Path.GetFileNameWithoutExtension(imageFile.FileName); //02animalpicture
            var fileExtension = Path.GetExtension(imageFile.FileName); //.jpeg
            var finalFileName = $"{filename}_{DateTime.Now.ToString("yyMMddHHmmssff")}{fileExtension}";
            var filePath = Path.Combine(rootPath, "images", finalFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return finalFileName;
        }
        // GET: RestMenus/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
                ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id");
            //    ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id");
            //    return View();
            var restMenusM = new RestMenusModels();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var categories = await _context.FoodCategories.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            }).ToListAsync();
            var RestNames = await _context.RestInfo.Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.RestName
            }).ToListAsync();

            restMenusM.Categories = categories;
            restMenusM.RestNames = RestNames;
            return View(restMenusM);
        }
        // POST: RestMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("id,Name,Composition,Price,CoocingTime,RestId,CategoryId")] RestMenu restMenu)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(restMenu);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    //ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id",  restMenu.CategoryId);
        //    //ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.Name);
        //    return View(restMenu);
        //}
        public async Task<IActionResult> Create(RestMenusModels model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string finalFileName = null;
            if (model.ImageFile != null)
            {
                finalFileName = await CopyFile(model.ImageFile);
            }
            if (model.Id != null)
            {

            }
            //Current user id
           // var currenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var rest  = new RestMenu
            {
                Id = Guid.NewGuid(),
                CategoryId =model.CategoryId,
                Name = model.Name,
                Composition = model.Composition,
                ImageName = finalFileName,
                CoocingTime = model.CoocingTime,
                Price = model.Price,
                RestId = model.RestId,
                InsertDataTime = DateTime.Now,    
                UpdateDate = null
            };
            _context.RestMenus.Add(rest);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "RestMenus");
            //return View(rest);
        }
        // GET: RestMenus/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restMenu = await _context.RestMenus.FindAsync(id);
            if (restMenu == null)
            {
                return NotFound();
            }
            ViewData["RestId"] = new SelectList(_context.RestMenus, "Id", "Id", restMenu.RestId);
            return View(restMenu);
        }
        // POST: RestMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("id,Name,Composition,Price,CoocingTime,RestId,CategoryId,")] RestMenu restMenu)
        {
            if (id != restMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestMenuExists(restMenu.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id",restMenu.CategoryId );
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            return View(restMenu);
        }
        // GET: RestMenus/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restMenu = await _context.RestMenus
                .Include(r => r.RestInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restMenu == null)
            {
                return NotFound();
            }
            return View(restMenu);
        }
        // POST: RestMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restMenu = await _context.RestMenus.FindAsync(id);
            _context.RestMenus.Remove(restMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool RestMenuExists(Guid id)
        {
            return _context.RestMenus.Any(e => e.Id == id);
        }
    }
}
