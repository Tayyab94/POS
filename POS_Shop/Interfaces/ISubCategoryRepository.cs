using POS_Shop.DTOs.City;
using POS_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface ISubCategoryRepository : IRepository<SubCategory>
    {
        Task<IEnumerable<SubListForDataGrifDto>> GetSubcategoriesListAsync();

        Task<bool> UpdateSubCategory(SubCategory model);
    }
}
