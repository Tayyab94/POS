using POS_Shop.DTOs.City;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
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
            return await _context.Products.AnyAsync(x => x.ProductEnglishName.Equals(name,StringComparison.OrdinalIgnoreCase));

        }

        public async Task<(int totalCount, IEnumerable<Product> data)> GetProductPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Products.AsQueryable();

            // apply search

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(s => s.ProductEnglishName.Contains(search) || s.Id.ToString().Contains(search));
            }
            var totalCount = await data.CountAsync();
            var result = await data.OrderBy(s => s.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            //.Select(s => new Product()
            //{
            //   Id= s.Id,
            //     ProductEnglishName= s.ProductEnglishName,
            //        ProductUrduName= s.ProductUrduName,
            //            Cost= s.Cost,
            //            SalePrice= s.SalePrice,
            //            PurchasePrice= s.PurchasePrice,
            //                SubcategoryId= s.SubcategoryId,
            //                ProductType= s.ProductType
            //}).ToListAsync();

            return (totalCount, result);
        }
    }
}
