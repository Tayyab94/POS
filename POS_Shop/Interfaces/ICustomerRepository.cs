using POS_Shop.DTOs.City;
using POS_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer> {

        Task<(int totalCount, IEnumerable<CustomerListForDataGridDto> data)> GetCustomerPagingListAsync(int pageIndex, int pageSize, string search);
        Task<bool> CheckRecoradAlreadyExistByName(string name);
    }
}
