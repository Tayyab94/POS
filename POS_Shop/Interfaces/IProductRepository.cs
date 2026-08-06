using POS_Shop.DTOs.City;
using POS_Shop.DTOs.Product;
using POS_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        //Task<bool> UpdateCategory(Category model);

        Task<(int totalCount, IEnumerable<Product> data)> GetProductPagingListAsync(int pageIndex, int pageSize, string search);
        Task<bool> CheckRecoradlreadyExistByName(string name);
        Task<IEnumerable<Product>> GetAll(List<int> ids);
        IEnumerable<ProductOrderHistoryDetails> ProductPreviousPriceInRecentOrderByCustomerId(int customerId, int productId);

        Task<(int totalCount, bool hasMore, IEnumerable<ProductListDtp> data)> GetProductCursorPagingListAsync(int? cursor, int pageSize, string search, bool showLessQty=false);

    }
}
