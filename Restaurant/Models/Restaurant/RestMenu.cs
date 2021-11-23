using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Composition { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string CoocingTime { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public DateTime? UpdateDate { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public DateTime InsertDataTime { get; set; }
        [Required]
        public Guid RestId { get; set; }
        [ForeignKey("RestId")]
        public virtual RestInfo RestInfo { get; set; }
        //public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual FoodCategory FoodCategory { get; set; }
    }
}
