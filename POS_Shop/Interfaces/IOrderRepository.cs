using POS_Shop.DTOs.City;
using POS_Shop.DTOs.Order;
using POS_Shop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface IOrderRepository: IRepository<Order>
    {
        Task<int> AddOrder(Order order);
        Task<(int totalCount, IEnumerable<OrdersListDto> data)> GetOrderPagingListAsync(int pageIndex, int pageSize, string search);

    }
}
