using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class RestMenu
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        public decimal Price { get; set; }
        public string CoocingTime { get; set; }
        public int RestId { get; set; }
        [ForeignKey("RestId")]
        public virtual RestInfo RestInfo { get; set; }
    }
}
