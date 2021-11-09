using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviePortal.Services.Movie
{
    public class MovieService
    {
        private readonly RestaurantContext _context;
        private readonly IMapper _mapper;

        public MovieService(RestaurantContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<RestInfo>> GetAll()
        {
            var movies = await _context.RestInfo.Select(m => new RestInfo
            {
                //CategoryId = m.CategoryId,
                //CategoryName = m.MovieCategory.Name,
                //Description = m.Description,
                //Director = m.Director,
                //Id = m.Id,
                //ImageName = m.Image,
                //InsertDateTime = m.InsertDateTime,
                //InsertUserId = m.InsertUserId,
                //ReleaseDate = m.ReleaseDate,
                //Title = m.Title,
                //UpdateDate = m.UpdateDate,
                //UserId = m.User.Id,
                //UserName = m.User.UserName
            }).ToListAsync();

            return movies;
        }

        public async Task<RestInfo> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new Exception("Movie with this id not found");
            }

            var rest = await _context.RestInfo.FirstOrDefaultAsync(p => p.Id.Equals(Guid.Parse(id)));

            if (rest == null)
            {
                throw new Exception("Movie with this id not found");
            }

            var restInfo = _mapper.Map<RestInfo>(rest);

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
