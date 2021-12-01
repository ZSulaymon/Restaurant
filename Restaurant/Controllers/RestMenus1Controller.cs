using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;

namespace Restaurant.Controllers
{
    public class RestMenus1Controller : Controller
    {
        private readonly RestaurantContext _context;

        public RestMenus1Controller(RestaurantContext context)
        {
            _context = context;
        }

        // GET: RestMenus1
        public async Task<IActionResult> Index()
        {
            var restaurantContext = _context.RestMenus.Include(r => r.FoodCategory).Include(r => r.RestInfo).Include(r => r.User);
            return View(await restaurantContext.ToListAsync());
        }

        // GET: RestMenus1/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restMenu = await _context.RestMenus
                .Include(r => r.FoodCategory)
                .Include(r => r.RestInfo)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (restMenu == null)
            {
                return NotFound();
            }

            return View(restMenu);
        }

        // GET: RestMenus1/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id");
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: RestMenus1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Composition,Price,CoocingTime,Description,ImageName,UpdateDate,InsertDataTime,RestId,CategoryId,UserId")] RestMenu restMenu)
        {
            if (ModelState.IsValid)
            {
                restMenu.Id = Guid.NewGuid();
                _context.Add(restMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id", restMenu.CategoryId);
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", restMenu.UserId);
            return View(restMenu);
        }

        // GET: RestMenus1/Edit/5
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
            ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id", restMenu.CategoryId);
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", restMenu.UserId);
            return View(restMenu);
        }

        // POST: RestMenus1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Composition,Price,CoocingTime,Description,ImageName,UpdateDate,InsertDataTime,RestId,CategoryId,UserId")] RestMenu restMenu)
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
            ViewData["CategoryId"] = new SelectList(_context.FoodCategories, "Id", "Id", restMenu.CategoryId);
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", restMenu.UserId);
            return View(restMenu);
        }

        // GET: RestMenus1/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restMenu = await _context.RestMenus
                .Include(r => r.FoodCategory)
                .Include(r => r.RestInfo)
                .Include(r => r.User)
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
