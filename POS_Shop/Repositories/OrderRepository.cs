using DocumentFormat.OpenXml.Bibliography;
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
            if(order.Id == 0)
            {
                var orderData = new Order()
                {
                    CreatedDate = DateTime.Now,
                    TotalBill = order.TotalBill,
                    ReceiveAmount = order.ReceiveAmount,
                    InvoiceNumber = order.InvoiceNumber,
                    customerId = order.customerId > 0 ? order.customerId : null,
                    paymentType = order.paymentType,
                };
                _context.Orders.Add(order);
                _context.SaveChanges();
                return order.Id;
            }

            var prevOrder= await _context.Orders.Where(s=>s.Id== order.Id && s.InvoiceNumber==order.InvoiceNumber).FirstOrDefaultAsync();
            if(prevOrder!=null)
            {
                prevOrder.CreatedDate = DateTime.Now;
                prevOrder.TotalBill = order.TotalBill;
                prevOrder.ReceiveAmount = order.ReceiveAmount;
                prevOrder.InvoiceNumber = order.InvoiceNumber;
                prevOrder.customerId = order.customerId > 0 ? order.customerId : null;
                prevOrder.paymentType = order.paymentType;
            }
            _context.Entry(prevOrder).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return prevOrder.Id;
           
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id, string invoiceNo)
        {
            var data = await  _context.Orders.Where(s=>s.Id== id && s.InvoiceNumber==invoiceNo)
                .Include(s=>s.Customer)
                .Include(s=>s.OrderDetails)
                .Select(s=> new OrderDto()
                {
                    Id = s.Id,
                    InvoiceNumber = s.InvoiceNumber,
                     CreatedDate= s.CreatedDate,
                     TotalBill = s.TotalBill,
                     ReceiveAmount = s.ReceiveAmount,
                     CustomerId=s.customerId,
                     CustomerName= s.Customer.CustomerName,
                      paymentType=s.paymentType,
                      OrderDetailsList= s.OrderDetails.Select(o=>new OrderDetailDto()
                      {  Id = o.Id,
                       Price = o.Price,
                        ProductId
                        =o.ProductId,
                         ProductName=o.ProductId.HasValue? o.Product.ProductUrduName: o.OtherProductName,
                         Quantity = o.Quantity,
                         QuantityType = o.QuantityType,
                            
                      }).ToList()
                      
                }).FirstOrDefaultAsync();

            return data;
        }

        public async Task<(int totalCount, IEnumerable<OrdersListDto> data)> GetOrderPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Orders.Include(s=>s.Customer).AsQueryable();

            // apply search


            var searchWords = search.ToLower().Split(' ');
            // apply search

            foreach (var word in searchWords)
            {
                data = data.Where(s => s.Id.ToString().Contains(word) || s.InvoiceNumber.ToString().Contains(word) || s.Customer.CustomerName.Contains(word) || s.Customer.CustomerAddress.Contains(word));
                //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
            }
           
            var totalCount = await data.CountAsync();

            var result = await data.OrderByDescending(s => s.Id)
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
