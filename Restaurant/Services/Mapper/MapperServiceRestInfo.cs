using AutoMapper;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Services.Mapper
{
    public class MapperServiceRestInfo: Profile 
    {
        public MapperServiceRestInfo()
        {
            CreateMap<RestInfo, RestInfoModels>();
        }
    }
}
