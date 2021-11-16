using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using Restaurant.Services.RestMenus;

namespace Restaurant.Controllers
{
    public class RestMenusController : Controller
    {
        private readonly RestaurantContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RestMenusService _restMenusService;


        public RestMenusController(RestaurantContext context,
            IWebHostEnvironment webHostEnvironment, 
            RestMenusService restMenusService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
           _restMenusService = restMenusService;

        }


        // GET: RestMenus1
        public async Task<IActionResult> Index()
        {
            var restMenu = await _restMenusService.GetAll();
            return View(restMenu);
        }

        // GET: RestMenus1/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restMenu = await _restMenusService.GetById(id);
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
        // GET: RestMenus1/Create
        public async Task<IActionResult> CreateAsync()
        {
            // ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id");
            // ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id");
            var restMenusM = new RestMenusModels();
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

        // POST: RestMenus1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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
            var rest = new RestMenu
            {
                Id = Guid.NewGuid(),
                CategoryId = model.CategoryId,
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
 
        }

        // GET: RestMenus1/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //var restmenu = await _restMenusService.GetById(id);
            //return View(restmenu);
            if (id == null)
            {
                return NotFound();
            }
            var rest = await _context.RestMenus.FindAsync(id);
            if (rest == null)
            {
                return RedirectToAction("Index");
            }
            var result = new RestMenusModels
            {
                Id = rest.Id,
                RestName = rest.RestInfo.RestName,
                Name = rest.Name,
                Price = rest.Price,
                Composition = rest.Composition,
                CoocingTime = rest.CoocingTime,
                ImageName = rest.ImageName,
                //CategoryName  = rest.FoodCategory.Name,
                Categories = await _context.FoodCategories.
                     Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }).ToListAsync(),
                RestNames = await _context.RestInfo.
                     Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.RestName }).ToListAsync()
            };
            return View(result);
        }

       
        // POST: RestMenus1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RestMenu model)
        {
            var fileName = "";
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.ImageFile != null)
            {
                await DeleteFile(model.ImageName, model.Id.ToString());
                fileName = await CopyFile(model.ImageFile);
            }
            await _restMenusService.Update(model, fileName);

            //ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id", restMenu.CategoryId);
            //ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            return RedirectToAction("Index");

            //return View();
        }
        public async Task<IActionResult> DeleteImage(string imageName, string restId)
        {
            if (string.IsNullOrEmpty(imageName) || string.IsNullOrEmpty(restId))
            {
                return BadRequest();
            }

            var result = await DeleteFile(imageName, restId);

            if (!result)
            {
                return BadRequest();
            }

            return Ok("Success");
        }
        [NonAction]
        private async Task<bool> DeleteFile(string imageName, string menuId)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                return false;
            }
            var rootPath = _webHostEnvironment.WebRootPath;
            var finalfilePath = Path.Combine(rootPath, "images", imageName);
            if (!System.IO.File.Exists(finalfilePath))
            {
                return false;
            }
            await Task.Run(() =>
            {
                System.IO.File.Delete(finalfilePath);
            });
            var parsedMovieIdResult = Guid.TryParse(menuId, out var parsedMovieId);
            if (!parsedMovieIdResult)
            {
                return false; ;
            }
            var rest = await _context.RestMenus.FirstOrDefaultAsync(p => p.Id.Equals(parsedMovieId));
            if (rest == null)
            {
                return false;
            }
            rest.ImageName = null;
            await _context.SaveChangesAsync();
            return true;
        }
        // GET: RestMenus1/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           // var result = await DeleteFile(imageName, menuId);
            var restMenu = await _context.RestMenus
                .Include(r => r.FoodCategory)
                .Include(r => r.RestInfo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restMenu == null)
            {
                return NotFound();
            }
            return View(restMenu);
        }
        // POST: RestMenus1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restMenu = await _context.RestMenus.FindAsync(id);
            var imageName = restMenu.ImageName;
            var menuId = id;
            var result = await DeleteFile(imageName, menuId.ToString());
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
