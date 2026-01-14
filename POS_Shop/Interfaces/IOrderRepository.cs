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

        Task<OrderDto> GetOrderByIdAsync(int id, string invoiceNo);

        Task<string>AddTempOrder(TempOrder tempOrder);

        Task<(int totalCount, IEnumerable<TempOrderListDto> data)> GetTempOrderPagingListAsync(int pageIndex, int pageSize, string search);
        Task<(int totalCount, bool hasMore, IEnumerable<OrdersListDto> data)> GetOrderCursorPagingListAsync(int? cursor, int pageSize, string search);
        List<TempOrderDetail>GetTempOrderDetailByInvoice(string invoiceNo);
       OrderAmountSummaryDto GetLatestOrderAmountSummaryByCustomerId(int customerId);
    }
}
