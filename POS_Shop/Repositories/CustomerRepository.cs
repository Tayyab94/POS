using POS_Shop.DTOs.City;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(POSDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckRecoradAlreadyExistByName(string name, string address) => await _context.Customers.AnyAsync(x => x.CustomerName.Equals(name, StringComparison.OrdinalIgnoreCase) && x.CustomerAddress.Equals(address, StringComparison.OrdinalIgnoreCase));

        public async Task<IEnumerable<Customer>> GetAll(List<int> ids)
        {
            var data = await _context.Customers.Where(e => ids.Contains(e.Id)).ToListAsync();
            return data;
        }

        public async Task<(int totalCount, IEnumerable<CustomerListForDataGridDto> data)> GetCustomerPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Customers
                .Include(s => s.City)
                .AsNoTracking()
                .AsQueryable();

            //var searchWords = search.ToLower().Split(' ');
            //// apply search

            //foreach (var word in searchWords)
            //{
            //    data = data.Where(s => s.CustomerName.Contains(word) || s.CustomerAddress.Contains(word) || s.City.Name.Contains(word));
            //}
            // Apply search filter if provided
            if (!string.IsNullOrWhiteSpace(search))
            {
                // Split once and filter out empty entries
                var searchWords = search.Trim()
                    .ToLower()
                    .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                // Build predicate efficiently
                foreach (var word in searchWords)
                {
                    string currentWord = word; // Capture variable for closure
                    data = data.Where(s =>
                        s.CustomerName.ToLower().Contains(currentWord) ||
                        s.CustomerAddress.ToLower().Contains(currentWord) ||
                        s.City.Name.ToLower().Contains(currentWord)
                    );
                }
            }

            var totalCount = await data.CountAsync();
            var result = await data.OrderByDescending(s => s.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s=>new CustomerListForDataGridDto()
                {
                     Id= s.Id,
                      CustomerName= s.CustomerName,
                       ContactNo= s.ContactNo,
                        CustomerAddress= s.CustomerAddress,
                          CityName= s.City.Name,
                           IsDeleted= s.IsDeleted,
                           CityId= s.CityId
                }).ToListAsync();

            return (totalCount, result);
        }
    }
}
