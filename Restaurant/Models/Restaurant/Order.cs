using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models.Restaurant
{
    public class Order
    {
        [BindNever]                                     //никогда не будет показано на страничке.
        public Guid Id { get; set; }
        [Display(Name = "Введите имя")]
        [StringLength(25)]
        [Required(ErrorMessage = "Длина имя не менее 5 символов")]
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        public int Phone { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage ="Email не указан верно")]
        public string Email { get; set; }
        [BindNever]
        [ScaffoldColumn(false)]                         //не показывать данное поле нигде.
        public DateTime OrderTime { get; set; }
        public virtual List<OrderDetail>  OrderDetails { get; set; }
    }
}
