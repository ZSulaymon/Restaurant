using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using Restaurant.Services.RestMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly RestaurantContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RestMenusService _restMenusService;

        private readonly ShopCart _shopCart;


        public ShopCartController(RestaurantContext context,
            IWebHostEnvironment webHostEnvironment,
            RestMenusService restMenusService,
            ShopCart shopCart)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _restMenusService = restMenusService;
            _shopCart = shopCart;

        }
        public ViewResult Index()
        {
            var items = _shopCart.getShopItems();
             _shopCart.listShopItems = items;
            //_shopCart.listShopItems.


            var obj = new ShopCartmodels
            {
                shopCart = _shopCart
            };
           // var count = items.Count;

           // ViewBag.Count = items.Count;
                
            return View(obj);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
        //public IActionResult AddToCart(Guid id)
        public async Task<RedirectToActionResult> AddToCart(Guid? id)

        {       
            var items =  _shopCart.getShopItems(id);
            var menu =  _context.RestMenus.FirstOrDefault(i => i.Id == id);

             ShopCartItem item = new ShopCartItem();
            if (items.Count >0)
            {
                item = items.Find(x => x.RestMenu.Id == id);
            }
            if (item != null && item.RestMenu?.Id == id)
            {
              
               await _shopCart.UpAddToCart(item);          
            }
            else
            {
                await _shopCart.AddToCart(menu);
            }
            return RedirectToAction("Index");
         }
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoopCart = await _context.ShopCartItems.FindAsync(id);
 
            _context.ShopCartItems.Remove(shoopCart);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

         }        
    }
}
