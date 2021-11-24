﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Context;
using Restaurant.Services.RestMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class ShopCart
    {    
        private readonly RestaurantContext _context;
        private readonly RestMenusService _restMenusService;

        //public ShopCart(RestaurantContext context,
        //    RestMenusService restMenusService)
        //{
        //    _context = context;
        //    _restMenusService = restMenusService;
        //}
        public ShopCart(RestaurantContext context)
        {
            this._context = context;
        }

        public string ShopCartId { get; set; }
        public List<ShopCartItem> listShopItems { get; set; }

        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<RestaurantContext>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            
            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId }; 
        }
        public void AddToCart(RestMenu RestMenu)
        {
           
            _context.ShopCartItems.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                RestMenu = RestMenu,
                Price = RestMenu.Price,
                Quantity = 1,
                Total = RestMenu.Price
            }) ;
            _context.SaveChanges();
        }
        public void UpAddToCart(RestMenu RestMenu)
        {
            // var item = _context.RestMenus.FirstOrDefault(i => i.Id == id);
            // 
            // SelectList persons = new SelectList(_context.listShopItems, "Id", "FirstName");

            //  ViewBag.persons = persons;
            //if (RestMenu.Id == )
            //{
            //    var count = listShopItems.ToString();
            //}
            //foreach ( cust in _context.ShopCartItems)
            //{
            //    if (cust.IsValid)
            //    {
            //        cust.CreditLimit = 1000;
            //    }
            //}
            var Index = 1;
            _context.ShopCartItems.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                RestMenu = RestMenu,
                Price = RestMenu.Price,
                Quantity = Index,
            }) ;
            _context.SaveChanges();
         }
        public List<ShopCartItem> getShopItems() 
        {
            return _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.RestMenu).ToList();
        }
    }
}