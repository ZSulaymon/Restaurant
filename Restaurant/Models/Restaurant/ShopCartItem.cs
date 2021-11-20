using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class ShopCartItem
    {
        public Guid Id { get; set; }
        public RestMenu RestMenu { get; set; }
        public decimal Price { get; set; }
        public string ShopCartId { get; set; }
    }
}
