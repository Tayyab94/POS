using POS_Shop.DTOs.Supplier;
using POS_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface ISupplierRepository : IRepository<Supplier>
    {

        Task<(int totalCount, IEnumerable<SupplierListForDataGridDto> data)> GetSupplierPagingListAsync(int pageIndex, int pageSize, string search);
        Task<bool> CheckRecoradAlreadyExistByName(string name, string address);

        Task<IEnumerable<Supplier>> GetAll(List<int> ids);
    }
}
