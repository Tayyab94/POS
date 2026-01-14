using POS_Shop.DTOs.Order;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class OrderRepository : Repository<Models.Order>, IOrderRepository
    {
        public OrderRepository(POSDbContext context) : base(context) { }

        public async Task<int> AddOrder(Models.Order order)
        {
            if(order.Id == 0)
            {
                var orderData = new Models.Order()
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
                    _context.Entry(prevOrder).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
            }

            return order.Id;
        }

        public async Task<string> AddTempOrder(TempOrder order)
        {



            //var orderData = new TempOrder()
            //{
            //    CreatedDate = DateTime.Now,
            //    TotalBill = order.TotalBill,
            //    InvoiceNumber = order.InvoiceNumber,
            //    customerId = order.customerId > 0 ? order.customerId : null,
            //    CustomerName = order.CustomerName,

            //};
            //_context.TempOrders.Add(order);
            //_context.SaveChanges();
            //return order.InvoiceNumber;

            // Check if a record with the same InvoiceNumber already exists
            var existingOrder = await _context.TempOrders
                .FirstOrDefaultAsync(o => o.InvoiceNumber == order.InvoiceNumber);

            if (existingOrder != null)
            {
                // Update existing record
                existingOrder.CreatedDate = DateTime.Now;
                existingOrder.TotalBill = order.TotalBill;
                existingOrder.customerId = order.customerId > 0 ? order.customerId : null;
                existingOrder.CustomerName = order.CustomerName;
                _context.Entry(existingOrder).State = EntityState.Modified;
               
            }
            else
            {
                // Add new record
                var orderData = new TempOrder()
                {
                    CreatedDate = DateTime.Now,
                    TotalBill = order.TotalBill,
                    InvoiceNumber = order.InvoiceNumber,
                    customerId = order.customerId > 0 ? order.customerId : null,
                    CustomerName = order.CustomerName,
                };
                _context.TempOrders.Add(orderData);
            }

            await _context.SaveChangesAsync();
            return order.InvoiceNumber;
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
                           ProductDetail= o.ProductDetail
                      }).ToList()
                      
                }).FirstOrDefaultAsync();

            return data;
        }

        //public async Task<(int totalCount, IEnumerable<OrdersListDto> data)> GetOrderPagingListAsync(int pageIndex, int pageSize, string search)
        //{
        //    var data = _context.Orders.Include(s=>s.Customer).AsNoTracking().AsQueryable();

        //    // apply search

        //    if(!string.IsNullOrWhiteSpace(search))
        //    {
        //        var searchWords = search.Trim().ToLower().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //        // apply search

        //        //foreach (var word in searchWords)
        //        //{
        //        //    data = data.Where(s => s.Id.ToString().Contains(word)
        //        //    || s.InvoiceNumber.ToString().ToLower().Contains(word)
        //        //    || s.Customer.CustomerName.ToLower().Contains(word)
        //        //    || s.Customer.CustomerAddress.ToLower().Contains(word));
        //        //    //data = data.Where(s => s.CustomerName.Contains(word) || s.City.Name.Contains(word));
        //        //}

        //        // Build a single WHERE clause with all conditions
        //        data = data.Where(s =>
        //            searchWords.Any(word =>
        //                SqlFunctions.StringConvert((double)s.Id).Contains(word) ||
        //                s.InvoiceNumber.ToLower().Contains(word) ||
        //                (s.Customer != null && (
        //                    s.Customer.CustomerName.ToLower().Contains(word) ||
        //                    s.Customer.CustomerAddress.ToLower().Contains(word)
        //                ))
        //            )
        //        );

        //    }

        //    var totalCount = await data.CountAsync();

        //    var result = await data.OrderByDescending(s => s.Id)
        //        .Skip((pageIndex - 1) * pageSize)
        //        .Take(pageSize)
        //        .Select(s => new OrdersListDto()
        //        {
        //            Id = s.Id,
        //            InvoiceNumber = s.InvoiceNumber,
        //          paymentType=s.paymentType,
        //           CreatedDate = s.CreatedDate,
        //           customerId= s.customerId.HasValue? s.customerId: null,
        //            CustomerName = s.customerId == null? "No":s.Customer.CustomerName.ToString(),
        //            ReceiveAmount= s.ReceiveAmount, 
        //             TotalBill  = s.TotalBill,
        //        }).ToListAsync();

        //    return (totalCount, result);
        //}

        public async Task<(int totalCount, IEnumerable<OrdersListDto> data)> GetOrderPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Orders.Include(s => s.Customer).AsNoTracking().AsQueryable();

            // apply search
            var searchWords = search.ToLower().Split(' ');
            // apply search

            foreach (var word in searchWords)
                data = data.Where(s => s.Id.ToString().Contains(word) || s.InvoiceNumber.ToString().Contains(word) || s.Customer.CustomerName.Contains(word) || s.Customer.CustomerAddress.Contains(word));

            var totalCount = await data.CountAsync();

            var result = await data.OrderByDescending(s => s.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new OrdersListDto()
                {
                    Id = s.Id,
                    InvoiceNumber = s.InvoiceNumber,
                    paymentType = s.paymentType,
                    CreatedDate = s.CreatedDate,
                    customerId = s.customerId.HasValue ? s.customerId : null,
                    CustomerName = s.customerId == null ? "No" : s.Customer.CustomerName.ToString(),
                    ReceiveAmount = s.ReceiveAmount,
                    TotalBill = s.TotalBill,
                }).ToListAsync();

            return (totalCount, result);
        }

        public List<TempOrderDetail> GetTempOrderDetailByInvoice(string invoiceNo)
        {
            var data= _context.TempOrderDetails.Where(s => s.TempInvoiceNumber == invoiceNo)
                .ToList();
            return data;
        }

        public async Task<(int totalCount, IEnumerable<TempOrderListDto> data)> GetTempOrderPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.TempOrders.AsQueryable();

            var searchWords = search.ToLower().Split(' ');
            // apply search

            foreach (var word in searchWords)
            {
                data = data.Where(s => s.CustomerName.ToString().Contains(word));
            }

            var totalCount = await data.CountAsync();

            var result = await data.OrderByDescending(s => s.InvoiceNumber)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new TempOrderListDto()
                {
                    InvoiceNumber = s.InvoiceNumber,
                    CreatedDate = s.CreatedDate,
                    customerId = s.customerId.HasValue ? s.customerId : null,
                    CustomerName = s.CustomerName.ToString(),
                    TotalBill = s.TotalBill,
                }).ToListAsync();

            return (totalCount, result);
        }

        public OrderAmountSummaryDto GetLatestOrderAmountSummaryByCustomerId(int customerId)
        {

            var data = _context.Orders.Where(s => s.customerId == customerId).OrderByDescending(s => s.Id)
                .Select(s => new OrderAmountSummaryDto()
                {
                    TotalAmount = s.TotalBill,
                    ReceivedAmount = s.ReceiveAmount
                }).FirstOrDefault();
            return data;

        }

        public async Task<(int totalCount, bool hasMore, IEnumerable<OrdersListDto> data)> GetOrderCursorPagingListAsync(int? cursor, int pageSize, string search)
        {
            var query = _context.Orders.Include(s => s.Customer).AsNoTracking().AsQueryable();

            // apply search
            var searchWords = search.ToLower().Split(' ');
            // apply search

            foreach (var word in searchWords)
                query = query.Where(s => s.Id.ToString().Contains(word) || s.InvoiceNumber.ToString().Contains(word) || s.Customer.CustomerName.Contains(word) || s.Customer.CustomerAddress.Contains(word));

            var totalCount = await query.CountAsync();

            // Apply cursor filter (fetch records after the cursor)
            if (cursor.HasValue)
            {
                query = query.Where(s => s.Id < cursor.Value);
            }

            // Fetch pageSize + 1 to determine if there are more records
            var result = await query
                .OrderByDescending(s => s.Id)
                .Take(pageSize + 1)
                .Select(s => new OrdersListDto()
                {
                    Id = s.Id,
                    InvoiceNumber = s.InvoiceNumber,
                    paymentType = s.paymentType,
                    CreatedDate = s.CreatedDate,
                    customerId = s.customerId.HasValue ? s.customerId : null,
                    CustomerName = s.customerId == null ? "No" : s.Customer.CustomerName.ToString(),
                    ReceiveAmount = s.ReceiveAmount,
                    TotalBill = s.TotalBill,
                })
                .ToListAsync();

            // Check if there are more records
            bool hasMore = result.Count > pageSize;

            // Return only pageSize records
            var data = hasMore ? result.Take(pageSize).ToList() : result;

            return (totalCount, hasMore, data);
        }
    }
}
