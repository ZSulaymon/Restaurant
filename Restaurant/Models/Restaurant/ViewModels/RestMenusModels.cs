using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant.ViewModels
{
    public class RestMenusModels
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Composition { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string CoocingTime { get; set; }
        public string Description { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime InsertDataTime { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> RestNames { get; set; } = new List<SelectListItem>();
        public string ImageName { get; set; }
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
        [Required]
        public Guid RestId { get; set; }
        public string RestName { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
     }
}
