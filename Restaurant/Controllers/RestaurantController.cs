using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Restaurant.Services.RestInfos;
using Microsoft.AspNetCore.Authorization;
using Restaurant.Models.Restaurant.ViewModels;

namespace Restaurant.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly RestaurantContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RestInfoService _restInfoService;

        public RestaurantController(RestaurantContext context,
            IWebHostEnvironment webHostEnvironment, RestInfoService restInfoService)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _restInfoService = restInfoService;
        }
        public  string GetCurrentUsertId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
            //return userId;
        }
         // GET: Restaurant
        public async Task<IActionResult> Index()
        {
             var rest = await _restInfoService.GetAll(GetCurrentUsertId());
            return View(rest);
        }

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
 
            var restDet = await _restInfoService.GetById(id);
  
            return View(restDet);
        }
        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RestInfo model)
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
            //var currenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           // var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var restInfo = new RestInfo
            {
                RestName = model.RestName,
                RestAdministrator  = model.RestAdministrator,
                RestPhone  = model.RestPhone,
                ImageName = finalFileName,
                InsertDateTime = DateTime.Now,
                UserId = GetCurrentUsertId(),
                RestAddress = model.RestAddress,
                Tables = model.Tables,
                RestReferencePoint = model.RestReferencePoint,
                Description = model.Description,
                UpdateDate = null
                 
            };
            _context.Add(restInfo);
                   await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(Index));
        }
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
        private async Task<bool> DeleteFile(string imageName, string RestId)
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
            var parsedRestIdInfoResult = Guid.TryParse(RestId, out var parsedRestId);
            if (!parsedRestIdInfoResult )
            {
                return false; ;
            }
            var restinfo = await _context.RestInfo.FirstOrDefaultAsync(p => p.Id.Equals(parsedRestId));
            if (restinfo == null)
            {
                return false;
            }
            restinfo.ImageName = null;
            await _context.SaveChangesAsync();
            return true;
        }
        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
             if (id == null)
            {
                return View(id);
            }
            var restMenu = await _context.RestInfo.FindAsync(id);
            if (restMenu == null)
            {
                return NotFound();
            }
            return View(restMenu);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RestInfo model)
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
            await _restInfoService.Update(model, fileName);
            //ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id", restMenu.CategoryId);
            //ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            return RedirectToAction("Index");
            //return View();
        }

        // GET: Restaurant/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var restInfo = await _context.RestInfo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restInfo == null)
            {
                return NotFound();
            }
            return View(restInfo);
        }
        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var restInfo = await _context.RestInfo.FindAsync(id);
            var imageName = restInfo.ImageName;
            var menuId = id;
            var result = await DeleteFile(imageName, menuId.ToString());
            _context.RestInfo.Remove(restInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestInfoExists(Guid id)
        {
            return _context.RestInfo.Any(e => e.Id == id);
        }
    }
}
