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

namespace Restaurant.Services.RestInfos
{
    public class RestInfoService
    {
        private readonly RestaurantContext _context;
        private readonly IMapper _mapper;

        public RestInfoService(RestaurantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RestInfoModels>> GetAll()
        {
            var movies = await _context.RestInfo.Select(m => new RestInfoModels
            {
                Id = m.Id,
                RestName = m.RestName,
                ImageName = m.ImageName,
                InsertDateTime = m.InsertDateTime,
                RestAddress = m.RestAddress,
                RestAdministrator = m.RestAdministrator,
                RestPhone = m.RestPhone,
                RestReferencePoint = m.RestReferencePoint,
                Description = m.Description,
                Kitchen = m.Kitchen,
                UpdateDate = m.UpdateDate,
                UserId = m.UserId
            }).ToListAsync();
            return movies;
        }

        public async Task<RestInfoModels> GetById(Guid? id)
        {
            if (id == null)
            {
                throw new Exception("Rest with this id not found");
            }

            var rest = await _context.RestInfo.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (rest == null)
            {
                throw new Exception("Rest with this id not found");
            }

            var restInfo = _mapper.Map<RestInfoModels>(rest);

            //movieDTO.Categories = await _context.MovieCategories.Select(p => new SelectListItem { Text = p.Name, Value = p.Id.ToString() }).ToListAsync();

            return restInfo;
        }

        public async Task Update(RestInfo model, string fileName)
        {
            //var movie = await _context.Movies.FirstOrDefaultAsync(p => p.Id.Equals(model.Id));

            //if (movie == null)
            //{
            //    throw new Exception($"Movie with id: {model.Id} not found");
            //}

            var rest = _mapper.Map<RestInfo>(model);

            rest.ImageName = string.IsNullOrEmpty(fileName) ? rest.ImageName : fileName;

            rest.UpdateDate = DateTime.Now;

            _context.RestInfo.Update(rest);

            await _context.SaveChangesAsync();
        }
    }
}
