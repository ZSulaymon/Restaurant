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
        private readonly HomeController _homeController;

        private readonly ShopCart _shopCart;


        public ShopCartController(RestaurantContext context,
            IWebHostEnvironment webHostEnvironment,
            RestMenusService restMenusService,
            ShopCart shopCart,
            HomeController homeController)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _restMenusService = restMenusService;
            _shopCart = shopCart;
            _homeController = homeController;
        }
        public void CallGetCountItems()
        {
            var count = _homeController.GetCountItems();
            ViewBag.Count = count;
            if (count == 0)
            {
                ViewBag.Message = "Корзина пуста, Перейдите на стариницу покупок!";
            }
        }
        public ViewResult Index()
        {
            var items = _shopCart.getShopItems();
            _shopCart.listShopItems = items;
            //_shopCart.listShopItems = _shopCart.getShopItems();
            //if (_shopCart.listShopItems.Count == 0)
            //{
            //    ViewBag.Message = "Корзина пуста, Перейдите на стариницу покупок!";
            //}
            CallGetCountItems();
            var obj = new ShopCartmodels
            {
                shopCart = _shopCart
            };
            return View(obj);
        }

        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
        //public IActionResult AddToCart(Guid id)
        public async Task<IActionResult> AddToCart(Guid id,Guid RestId)

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
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("GetMenu", "Home", new {id =  RestId});
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
