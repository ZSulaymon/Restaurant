using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Interfaces;
using Restaurant.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrders  _allOrders;
        private readonly ShopCart _shopCart;
        private readonly RestaurantContext _context;

        public OrderController(IAllOrders allOrders,
            ShopCart shopCart,
            RestaurantContext Context
            )
        {
            this._allOrders = allOrders;
            this._shopCart = shopCart;
            this._context = Context;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
        [HttpGet]
        //public IActionResult Index()
        //{
        //    return View();
        //}
        public async Task<IActionResult> Index()
        {
            var orders = await _context.OrderDetails.FirstOrDefaultAsync();

            //var restaurantContext = ;
            //return View(await restaurantContext.ToListAsync());
            //var allMenu = await _restMenusService.GetAll();
            return View(orders);
        }
        [HttpPost]
        public IActionResult Checkout(Order order)
        {
            _shopCart.listShopItems = _shopCart.getShopItems();
            if (_shopCart.listShopItems.Count == 0)
            {
                ModelState.AddModelError("","У Вас должны быть товары!");
            }
            if (ModelState.IsValid)
            {
                _allOrders.createOrder(order);
                // ViewBag.Message = "Заказ успешно обработан";
                //return RedirectToAction("Index", "Home","Complete");
                return RedirectToAction("Complete");
             }

            return View(order);
        }
        public IActionResult Complete()
        {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}
