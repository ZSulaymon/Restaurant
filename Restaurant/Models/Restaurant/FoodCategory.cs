using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class FoodCategory 
    {
        [Key]
        public Guid Id  { get; set; }
        public string Name { get; set; }
        public virtual ICollection<RestMenu>  RestMenus { get; set; }

    }
}
