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
        Task<IEnumerable<CitiesListForDataGridDto>> GetCitiesListAsync();

        Task<(int totalCount, IEnumerable<CitiesListForDataGridDto> data)> GetCitiesPagingListAsync(int pageIndex, int pageSize, string search);

        Task<bool>UpdateCity(City city);
    }
}
