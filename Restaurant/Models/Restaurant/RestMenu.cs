using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class RestMenu
    {
        [Key]
        public int id { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string CoocingTime { get; set; }
        public int RestId { get; set; }
        [ForeignKey("RestId")]
        public virtual RestInfo RestInfo { get; set; }
    }
}
