using POS_Shop.DTOs.City;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IEnumerable<CitiesListForDataGrifDto>> GetCitiesListAsync();

        Task<bool>UpdateCity(City city);
    }
}
