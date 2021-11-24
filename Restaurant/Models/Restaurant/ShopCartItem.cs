using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class ShopCartItem
    {
        public Guid Id { get; set; }
        public virtual RestMenu RestMenu { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public int  Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public string ShopCartId { get; set; }
    }
}
