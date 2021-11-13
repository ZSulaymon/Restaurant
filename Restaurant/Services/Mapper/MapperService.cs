using AutoMapper;
using Restaurant.Models.Account;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace Restaurant.Services.Mapper
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            CreateMap<RestMenu, RestMenusModels>()
                .ForMember(r => r.Id, option => option.MapFrom(r => r.Id))
                .ForMember(r => r.Name, option => option.MapFrom(r => r.Name))
                .ForMember(r => r.Composition, option => option.MapFrom(r => r.Composition))
                .ForMember(r => r.Price, option => option.MapFrom(r => r.Price))
                .ForMember(r => r.CoocingTime, option => option.MapFrom(r => r.CoocingTime))
                .ForMember(r => r.UpdateDate, option => option.MapFrom(r => r.UpdateDate))
                .ForMember(r => r.ImageFile, option => option.Ignore())
                //.ForMember(r => r.ImageFile, option => option.MapFrom(r=> r.ImageName))
                .ForMember(r => r.ImageName, option => option.MapFrom(r => r.ImageName))
                .ForMember(r => r.RestId, option => option.MapFrom(r => r.RestId))
                .ForMember(r => r.RestName, option => option.MapFrom(r => r.RestInfo.RestName))
                .ForMember(r => r.CategoryId, option => option.MapFrom(r => r.FoodCategory.Id))
                .ForMember(r => r.CategoryName, option => option.MapFrom(r => r.FoodCategory.Name))
                .ForMember(r => r.Categories, option => option.Ignore())
                .ForMember(r => r.RestNames, option => option.Ignore())
                .ReverseMap()
                .ForMember(r => r.Id, option => option.MapFrom(r => r.Id))
                .ForMember(r => r.Name, option => option.MapFrom(r => r.Name))
                .ForMember(r => r.Composition, option => option.MapFrom(r => r.Composition))
                .ForMember(r => r.Price, option => option.MapFrom(r => r.Price))
                .ForMember(r => r.CoocingTime, option => option.MapFrom(r => r.CoocingTime))
                .ForMember(r => r.UpdateDate, option => option.MapFrom(r => r.UpdateDate))
                .ForMember(r => r.ImageName, option => option.MapFrom(r => r.ImageName))
                .ForMember(r => r.RestId, option => option.MapFrom(r => r.RestId));
                //.ForMember(r => r.RestInfo.RestName, option => option.MapFrom(r => r.RestName))
                //.ForMember(r => r.FoodCategory.Id, option => option.MapFrom(r => r.CategoryId))
                //.ForMember(r => r.FoodCategory.Name, option => option.MapFrom(r => r.CategoryName));



        }
    }
}
