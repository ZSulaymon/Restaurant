using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Restaurant.Models.Account;

namespace Restaurant.Models.Restaurant
{
    public class RestInfo
    {
        [Key]
        public int Id { get; set; }
        public string RestName { get; set; }
        public string RestAddress { get; set; }
        public DateTime InsertDateTime { get; set; }
        public string RestReferencePoint { get; set; }
        public string RestPhone { get; set; }
        public string RestAdministrator { get; set; }
        public string ImageName { get; set; }
        [Display(Name = "Image")]
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

    }
}
