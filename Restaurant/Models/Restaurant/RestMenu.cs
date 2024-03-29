﻿using Microsoft.AspNetCore.Http;
using Restaurant.Models.Account;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


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
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        //public virtual  ShopCartItem ShopCartItem { get; set; }
        //public virtual ICollection<ShopCartItem> ShopCartItem { get; set; }

    }
}
