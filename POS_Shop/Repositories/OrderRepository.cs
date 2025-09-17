using DocumentFormat.OpenXml.Vml;
using POS_Shop.DTOs.Order;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(POSDbContext context) : base(context)
        {
        }

        public async Task<int> AddOrder(Order order)
        {
            var orderData = new Order()
            {
                CreatedDate = DateTime.Now,
                TotalBill = order.TotalBill,
                ReceiveAmount = order.ReceiveAmount,
                InvoiceNumber = order.InvoiceNumber,
                customerId = order.customerId > 0 ? order.customerId: null,
                paymentType = order.paymentType,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order.Id;
        }

        public async Task<(int totalCount, IEnumerable<OrdersListDto> data)> GetOrderPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Orders.AsQueryable();

            // apply search


            var searchWords = search.ToLower().Split(' ');
            // apply search

            foreach (var word in searchWords)
            {
                data = data.Where(s => s.Id.ToString().Contains(word) || s.InvoiceNumber.ToString().Contains(word));
                //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
            }
           
            var totalCount = await data.CountAsync();

            var result = await data.OrderBy(s => s.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new OrdersListDto()
                {
                    Id = s.Id,
                    InvoiceNumber = s.InvoiceNumber,
                  paymentType=s.paymentType,
                   CreatedDate = s.CreatedDate,
                    CustomerName= s.customerId == null? "No":s.Customer.CustomerName.ToString(),
                    ReceiveAmount= s.ReceiveAmount, 
                     TotalBill  = s.TotalBill,
                }).ToListAsync();

            return (totalCount, result);
        }
    }
}
