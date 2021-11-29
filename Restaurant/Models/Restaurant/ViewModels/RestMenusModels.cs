using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Restaurant.Models.Account;
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
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не можеть быть пустым")]
        public string Name { get; set; }
        [Display(Name = "Состав")]
        [Required(ErrorMessage = "Состав не можеть быть пустым")]
        public string Composition { get; set; }
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена не можеть быть пустым")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Display(Name = "Время приготовления")]
        [Required(ErrorMessage = "Время приготовления не можеть быть пустым")]
        public string CoocingTime { get; set; }
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Описание не можеть быть пустым")]
        public string Description { get; set; }
        [Display(Name="Время обновления")]
        public DateTime? UpdateDate { get; set; }
        [Display(Name = "Время создания")]
        public DateTime InsertDataTime { get; set; }
        public List<SelectListItem> Categories { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> RestNames { get; set; } = new List<SelectListItem>();
        //[Display(Name = "Картинка")]
        //[Required(ErrorMessage = "Картинка не можеть быть пустым")]
        public string ImageName { get; set; }
        [Display(Name = "Картинка")]
        [Required(ErrorMessage = "Картинка не можеть быть пустым")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Имя Ресторана")]
        [Required(ErrorMessage = "Имя ресторана не можеть быть пустым")]
        public Guid RestId { get; set; }
        [Display(Name = "Имя Ресторана")]
        //[Required(ErrorMessage = "Имя ресторана не можеть быть пустым")]
        public string RestName { get; set; }
        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Категория не можеть быть пустым")]
        public Guid CategoryId { get; set; }
        [Display(Name = "Имя Категория")]
         public string CategoryName { get; set; }
        public string UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
