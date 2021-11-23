using Microsoft.AspNetCore.Mvc;
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

        public OrderController(IAllOrders allOrders, ShopCart shopCart)
        {
            this._allOrders = allOrders;
            this._shopCart = shopCart;
        }

        //[HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
