using Restaurant.Context;
using Restaurant.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class OrdersRepository : IAllOrders
    {
        private readonly RestaurantContext _context;
        private readonly ShopCart _shopCart;

        public OrdersRepository(RestaurantContext Context, ShopCart shopCart)
        {
            this._context = Context;
            this._shopCart = shopCart;
        }

        public void createOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            _context.Orders.Add(order);

            var items = _shopCart.listShopItems;

            //foreach (var item in items)
            //{
            //    var shopSt = new ShopCartItem()
            //    {
            //        ShopStatus = "Заказано",
            //    };
            //    _context.ShopCartItems.Add(shopSt);
            //}                  

            foreach (var el in items)
            {
                var orderDetail = new OrderDetail()
                {
                    MenuId = el.RestMenu.Id,
                    OrderId = order.Id,
                    Price = el.RestMenu.Price,
                    UserId = el.RestMenu.UserId
                     
                };
                _context.OrderDetails.Add(orderDetail);
            }
            _context.SaveChanges();
        }
    }
}
