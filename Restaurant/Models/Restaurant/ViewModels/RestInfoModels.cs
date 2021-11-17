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
        public string RestName { get; set; }
        public string RestAddress { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string Description { get; set; }
        public string Kitchen { get; set; }
        public string RestReferencePoint { get; set; }
        public string RestPhone { get; set; }
        public string RestAdministrator { get; set; }
        [Display(Name = "Картинка")]
        public string ImageName { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
