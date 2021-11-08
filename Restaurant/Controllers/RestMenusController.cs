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
    public class RestMenusController : Controller
    {
        private readonly RestaurantContext _context;

        public RestMenusController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: RestMenus
        public async Task<IActionResult> Index()
        {
            var restaurantContext = _context.RestMenu.Include(r => r.RestInfo);
            return View(await restaurantContext.ToListAsync());
        }

        // GET: RestMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restMenu = await _context.RestMenu
                .Include(r => r.RestInfo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (restMenu == null)
            {
                return NotFound();
            }

            return View(restMenu);
        }

        // GET: RestMenus/Create
        public IActionResult Create()
        {
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id");
            return View();
        }

        // POST: RestMenus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Composition,Price,CoocingTime,RestId")] RestMenu restMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(restMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            return View(restMenu);
        }

        // GET: RestMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restMenu = await _context.RestMenu.FindAsync(id);
            if (restMenu == null)
            {
                return NotFound();
            }
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            return View(restMenu);
        }

        // POST: RestMenus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Composition,Price,CoocingTime,RestId")] RestMenu restMenu)
        {
            if (id != restMenu.id)
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
                    if (!RestMenuExists(restMenu.id))
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
            ViewData["RestId"] = new SelectList(_context.RestInfo, "Id", "Id", restMenu.RestId);
            return View(restMenu);
        }

        // GET: RestMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restMenu = await _context.RestMenu
                .Include(r => r.RestInfo)
                .FirstOrDefaultAsync(m => m.id == id);
            if (restMenu == null)
            {
                return NotFound();
            }

            return View(restMenu);
        }

        // POST: RestMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restMenu = await _context.RestMenu.FindAsync(id);
            _context.RestMenu.Remove(restMenu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestMenuExists(int id)
        {
            return _context.RestMenu.Any(e => e.id == id);
        }
    }
}
