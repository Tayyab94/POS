using POS_Shop.DTOs.City;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(POSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CitiesListForDataGridDto>> GetCitiesListAsync()
        {
            var data = await _context.Cities.Include(s => s.CountryId).Select(s => new CitiesListForDataGridDto()
            {
                CountryId= s.CountryId,
                CountryName = s.Country.CountryName,
                Id = s.Id,
                IsActive = s.IsActive,
                Name = s.Name,
            }).ToListAsync();

            return data;
        }

       public async Task<(int totalCount, IEnumerable<CitiesListForDataGridDto> data)> GetCitiesPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data =  _context.Cities.Include(s => s.CountryId).AsQueryable();
               
            // apply search
            
            if(!string.IsNullOrEmpty(search))
            {
                data = data.Where(s => s.Name.Contains(search) || s.Country.CountryName.Contains(search));
            }
            var totalCount = await data.CountAsync();
            var result= await data.OrderByDescending(s=>s.Id)
                .Skip((pageIndex -1)* pageSize)
                .Take(pageSize)
                .Select(s => new CitiesListForDataGridDto()
                {
                    CountryId = s.CountryId,
                    CountryName = s.Country.CountryName,
                    Id = s.Id,
                    IsActive = s.IsActive,
                    Name = s.Name,
                }).ToListAsync();

            return (totalCount, result);
        }

    

        public async Task<bool> UpdateCity(City city)
        {
            try
            {
                var data = GetById(city.Id);
                data.IsActive = city.IsActive;
                data.Name = city.Name;
                data.CountryId = city.CountryId;
                _context.Cities.Attach(data);
                _context.Entry(data).State = EntityState.Modified;
               await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
