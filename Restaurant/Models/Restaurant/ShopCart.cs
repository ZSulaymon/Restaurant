using Microsoft.AspNetCore.Hosting;
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
        public async Task AddToCart(RestMenu RestMenu)
        {
           
            _context.ShopCartItems.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                RestMenu = RestMenu,
                Price = RestMenu.Price,
                Quantity = 1,
                SubTotal = RestMenu.Price,
                MenuId = RestMenu.Id                               
            }) ;
            _context.SaveChanges();
        }
        public async Task UpAddToCart(ShopCartItem item)
        {
            //if (item != null && item.MenuId ==item.RestMenu.Id)
            if (item != null)
            {
                item.Quantity++;
                item.SubTotal = item.Quantity * item.Price;
            }
             _context.SaveChanges();
         }
        public List<ShopCartItem> getShopItems() 
        {
            return _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId).Include(s => s.RestMenu).ToList();
        }

        public List<ShopCartItem> getShopItems(Guid? id)
        {
            return _context.ShopCartItems.Where(c => c.ShopCartId == ShopCartId && c.RestMenu.Id ==id).ToList();
        }

        //public List<RestMenu> getShopItemss()
        //{
        //    return    _context.ShopCartItems.Where(c => c.MenuId == id || c.ShopCartId == ShopCartId).ToList();

        //}
    }
}
