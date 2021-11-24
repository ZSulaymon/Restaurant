using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public RedirectToActionResult AddToCart(Guid id)
        {
            var getListItems = _shopCart.getShopItems();
            //var product = _shopCart.listShopItems.Where(p => p.RestMenu.Id).SingleOrDefault(p => p.ItemId == itemId);
            //var getItems1 = getListItems.Where(c => (c.RestMenu.Id == id) ? false : true) ==null;
            //foreach (var el in getListItems)
            //{
            //    bool idcart = el.Id == id;
            //}
            //var getItems1 = getListItems.Where(c => c.RestMenu.Id == id);
            //return _context.ShopCartItems.Where(c => c.ShopCartId == ).Include(s => s.RestMenu).ToList();
            
             // _shopCart.listShopItems = items;
            //var count = items.Count;
            //var menuId = items

            //if (_context.RestMenus.Where(i=> i.Id == id) !=  null)
            //{
            //    //var count = listShopItems.ToString();
            //}
            var item = _context.RestMenus.FirstOrDefault(i => i.Id == id);
            //SelectList persons = new SelectList(item.Price, "Id", "Name");
            //item.Price
            //ViewBag.persons = persons;
            //var total = item. *
            //count = items.Count;
            //item. 

            if (getListItems.FirstOrDefault(i => i.RestMenu.Id == id) == null)
            {
                _shopCart.AddToCart(item);
            }
            else
            {
                //if (getListItems != null && getListItems.RestMenu.Id == id)
                //{ 
                    foreach (var el in getListItems)
                {
                    if (el !=null && el.RestMenu.Id == id)
                    {
                        el.Quantity = el.Quantity + 1;
                        el.Total = el.Quantity * el.Price;
                    }
                    _context.SaveChangesAsync();

                }
 
                //_shopCart.UpAddToCart(item);
            }
            // count = items.Count;

            return RedirectToAction("Index");

            //return RedirectToAction($"GetMenu", "Home");
        }

        //public List<ShopCartItem> getShopItems()
        //{
        //    return _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.RestMenu).ToList();
        //}

    }
}
