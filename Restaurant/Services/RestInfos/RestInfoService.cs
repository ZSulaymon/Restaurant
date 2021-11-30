using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.Context;
using Restaurant.Models.Account;
using Restaurant.Models.Restaurant;
using Restaurant.Models.Restaurant.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using Restaurant.Services.RestInfos;

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
        //public string GetCurrentUsertId()
        //{
        //    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    return userId;
        //}
        //public async Task<List<RestInfoModels>> GetAll(Func<string> id)
        public async Task<List<RestInfoModels>> GetAll(string id)
        {
            var rest = await _context.RestInfo.Select(m => new RestInfoModels
            {
                Id = m.Id,
                RestName = m.RestName,
                ImageName = m.ImageName,
                InsertDateTime = m.InsertDateTime,
                RestAddress = m.RestAddress,
                RestAdministrator = m.RestAdministrator,
                RestPhone = m.RestPhone,
                Tables = m.Tables,
                RestReferencePoint = m.RestReferencePoint,
                Description = m.Description,
                Kitchen = m.Kitchen,
                UpdateDate = m.UpdateDate,
                UserId = m.UserId
            }).Where(r=> r.UserId == id).ToListAsync();
            return rest;
        }
        public async Task<List<RestInfoModels>> GetAll()
        {
            var allRest = await _context.RestInfo.Select(m => new RestInfoModels
            {
                Id = m.Id,
                RestName = m.RestName,
                ImageName = m.ImageName,
                InsertDateTime = m.InsertDateTime,
                RestAddress = m.RestAddress,
                RestAdministrator = m.RestAdministrator,
                RestPhone = m.RestPhone,
                Tables = m.Tables,
                RestReferencePoint = m.RestReferencePoint,
                Description = m.Description,
                Kitchen = m.Kitchen,
                UpdateDate = m.UpdateDate,
                UserId = m.UserId,
                
                 
            }).ToListAsync();
            return allRest;
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


            return restInfo;
        }

        public async Task Update(RestInfo model, string fileName)
        {
            var rest = _mapper.Map<RestInfo>(model);

            rest.ImageName = string.IsNullOrEmpty(fileName) ? rest.ImageName : fileName;

            rest.UpdateDate = DateTime.Now;

            _context.RestInfo.Update(rest);

            await _context.SaveChangesAsync();
        }
    }
}
