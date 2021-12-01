using Microsoft.AspNetCore.Http;
using Restaurant.Models.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant.ViewModels
{
    public class RestInfoModels
    {
        public Guid Id { get; set; }
        [Display(Name ="Имя")]
        [Required(ErrorMessage ="Имя не можеть быть пустым")]
        public string RestName { get; set; }
        [Display(Name = "Адрес")]
        [Required(ErrorMessage = "Адрес не можеть быть пустым")]
        public string RestAddress { get; set; }
        //[Display(Name = "Имя")]
        //[Required(ErrorMessage = "Имя не можеть быть пустым")]
        public DateTime InsertDateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        [StringLength(90)]
        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Описание не можеть быть пустым")]
        //[Required(ErrorMessage = "Длина имя не менее 5 символов")]
        public string Description { get; set; }
        [Display(Name = "Кухня")]
        [Required(ErrorMessage = "Кухня не можеть быть пустым")]
        public string Kitchen { get; set; }
        [Display(Name = "Столы")]
        public int Tables { get; set; }
        [Display(Name = "Ориентир")]
        [Required(ErrorMessage = "Ориентир не можеть быть пустым")]
        public string RestReferencePoint { get; set; }
        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Телефон не можеть быть пустым")]
        public string RestPhone { get; set; }
        [Display(Name = "Администратор")]
        [Required(ErrorMessage = "Администратор не можеть быть пустым")]
        public string RestAdministrator { get; set; }
        [Display(Name = "Картинка")]
        public IFormFile ImageFile { get; set; }
       // [Required(ErrorMessage = "Картинка не можеть быть пустым")]
        public string ImageName { get; set; }
        public string UserId { get; set; }
        //public virtual User User { get; set; }
    }
}
