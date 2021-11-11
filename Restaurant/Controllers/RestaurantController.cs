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

        // GET: Restaurant
        public async Task<IActionResult> Index()
        {
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //if (userId == null)
            //{
            //    return NotFound();
            //}
            //var restInfo = await _context.RestInfo
            //    .FirstOrDefaultAsync(m=> m.Id = userId)
            return View(await _context.RestInfo.ToListAsync());
        }

        // GET: Restaurant/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

        // GET: Restaurant/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,RestName,RestAddress,RestReferencePoint,RestPhone,RestAdministrator,")] RestInfo restInfo)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(restInfo);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(restInfo);
        //}
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

            var currenUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           // var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var restInfo = new RestInfo
            {
                RestName = model.RestName,
                RestAdministrator  = model.RestAdministrator,
                RestPhone  = model.RestPhone,
                ImageName = finalFileName,
                InsertDateTime = DateTime.Now,
                UserId = currenUserId,
                RestAddress = model.RestAddress,
                RestReferencePoint = model.RestReferencePoint,
                UpdateDate = null
                 
            };

            _context.Add(restInfo);
                   await _context.SaveChangesAsync();
                   return RedirectToAction(nameof(Index));

            //_moviePortalContext.Movies.Add(movie);
            //await _moviePortalContext.SaveChangesAsync();

            //return RedirectToAction("Index", "Home");
        }

        // GET: Restaurant/Edit/5
        public async Task<IActionResult> Edit(RestInfo model )
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

             return View(model);
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

            var restinfo = await _context.RestMenus.FirstOrDefaultAsync(p => p.Id.Equals(parsedRestId));

            if (restinfo == null)
            {
                return false;
            }

            restinfo.ImageName = null;

            await _context.SaveChangesAsync();

            return true;
        }

        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,RestName,RestAddress,RestReferencePoint,RestPhone,RestAdministrator")] RestInfo restInfo)
        {
            if (id != restInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestInfoExists(restInfo.Id))
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
            return View(restInfo);
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restInfo = await _context.RestInfo.FindAsync(id);
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
