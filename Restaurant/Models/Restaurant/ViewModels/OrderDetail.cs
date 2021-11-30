using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class OrderDetail
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid MenuId { get; set; }
        public string UserId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [ForeignKey("MenuId")]
        public virtual RestMenu RestMenu { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Orders { get; set; }
    }
}
