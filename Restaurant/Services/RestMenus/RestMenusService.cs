using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Services.RestMenus
{
    public class RestMenusService
    {
        private readonly RestaurantContext _context;
        private readonly IMapper _mapper;

        public RestMenusService(RestaurantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RestMenusModels>> GetAll()
        {
            var menus = await _context.RestMenus.Select(m => new RestMenusModels
            {
                Name = m.Name,
                CategoryId = m.CategoryId,
                CoocingTime = m.CoocingTime,
                Price = m.Price,
                ImageName = m.ImageName,
                Composition = m.Composition,
                RestId = m.RestId,
                CategoryName = m.FoodCategory.Name,
                RestName = m.RestInfo.RestName,
                Id = m.Id,
            }).ToListAsync();
            return menus;
        }
        public async Task<RestMenusModels> GetById(Guid? id)
        {
            if (id == null)
            {
                throw new Exception("menu with this id not found");
            }

            var rest = await _context.RestMenus.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (rest == null)
            {
                throw new Exception("menu with this id not found");
            }

            var restMModels = _mapper.Map<RestMenusModels>(rest);

          //  restMModels.Categories = await _context.RestMenus.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToListAsync();

            return restMModels;
        }
        public async Task<RestMenusModels> GetMenuById(Guid? id)
        {
            if (id == null)
            {
                throw new Exception("menu with this id not found");
            }

            var rest = await _context.RestMenus.FirstOrDefaultAsync(p => p.RestId.Equals(id));

            if (rest == null)
            {
                throw new Exception("menu with this id not found");
            }

            var restMModels = _mapper.Map<RestMenusModels>(rest);

          //  restMModels.Categories = await _context.RestMenus.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToListAsync();

            return restMModels;
        }

        public async Task Update(RestMenu model, string fileName)
        {
            var rest = _mapper.Map<RestMenu>(model);

            rest.ImageName  = string.IsNullOrEmpty(fileName) ? rest.ImageName : fileName;

            rest.UpdateDate = DateTime.Now;

            _context.RestMenus.Update(rest);

            await _context.SaveChangesAsync();
        }
    }
}
