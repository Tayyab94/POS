using POS_Shop.DTOs.City;
using POS_Shop.DTOs.Product;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(POSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetAll(List<int> ids)
        {
            var data = await _context.Products.Where(e => ids.Contains(e.Id)).ToListAsync();
            return data;
        }
        public async Task<bool> CheckRecoradlreadyExistByName(string name)
        {
            //return await _context.Products.AnyAsync(x => x.ProductEnglishName.ToLower()== name.ToLower());
            return await _context.Products.AnyAsync(x => x.ProductEnglishName.Equals(name, StringComparison.OrdinalIgnoreCase));
        }   

        public async Task<(int totalCount, IEnumerable<Product> data)> GetProductPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Products.AsNoTracking().AsQueryable();

            // apply search

            if(!string.IsNullOrEmpty(search))
            {
                var searchWords = search.Trim().ToLower().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                // apply search

                foreach (var word in searchWords)
                {
                    //data = data.Where(s => s.ProductEnglishName.Contains(word) || s.Id.ToString().Contains(word) || s.SearchByProductCode.Contains(word));
                    data = data.Where(s => s.ProductEnglishName.ToLower().Contains(word) || s.Id.ToString().Contains(word) || s.SearchByProductCode.ToLower().Contains(word));

                }
            }
            var totalCount = await data.CountAsync();
            var result = await data.OrderBy(s => s.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            
            return (totalCount, result);
        }

        public IEnumerable<ProductOrderHistoryDetails> ProductPreviousPriceInRecentOrderByCustomerId(int customerId, int productId)
        {
            var data = new List<ProductOrderHistoryDetails>();
            using (var context = new POSDbContext())
                try
                {

                    var query = context.OrderDetails.AsNoTracking()
                       .Where(s => s.ProductId == productId && s.Order.customerId== customerId);

                    //// Only filter by customer if a specific customerId is provided
                    //if (customerId > 0)
                    //{
                    //    query = query.Where(s => s.Order.customerId == customerId);
                    //}

                    data = query
                       .OrderByDescending(s => s.OrderId)
                        .Take(4)
                        .AsEnumerable()
                        .Select(s => new ProductOrderHistoryDetails()
                        {
                            Price = s.Price,
                            TypeOfSale= s.QuantityType,
                            SaleDate = s.CreatedDate.ToString("dd-MMM-yyyy")
                        })
                        .ToList();
                  
                }
                catch (DbException ex)
                {
                    return data;
                }

            return data;
        }

        public async Task<(int totalCount, bool hasMore, IEnumerable<Product> data)> GetProductCursorPagingListAsync(int? cursor, int pageSize, string search)
        {

            var query = _context.Products.AsNoTracking().AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                var searchWords = search.Trim().ToLower()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var word in searchWords)
                {
                    query = query.Where(s =>
                        s.ProductEnglishName.ToLower().Contains(word) ||
                        s.Id.ToString().Contains(word) ||
                        s.SearchByProductCode.ToLower().Contains(word));
                }
            }

            // Get total count (cached if possible)
            var totalCount = await query.CountAsync();

            // Apply cursor filter (fetch records after the cursor)
            if (cursor.HasValue)
            {
                query = query.Where(s => s.Id > cursor.Value);
            }

            // Fetch pageSize + 1 to determine if there are more records
            var result = await query
                .OrderBy(s => s.Id)
                .Take(pageSize + 1)
                .ToListAsync();

            // Check if there are more records
            bool hasMore = result.Count > pageSize;

            // Return only pageSize records
            var data = hasMore ? result.Take(pageSize).ToList() : result;

            return (totalCount, hasMore, data);
        }
    }
}
