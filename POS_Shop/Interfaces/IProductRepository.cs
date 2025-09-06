using POS_Shop.DTOs.City;
using POS_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        //Task<bool> UpdateCategory(Category model);

       Task<(int totalCount, IEnumerable<Product> data)> GetProductPagingListAsync(int pageIndex, int pageSize, string search);
    }
}
