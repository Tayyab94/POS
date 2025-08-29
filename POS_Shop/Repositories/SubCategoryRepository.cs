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
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(POSDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SubListForDataGridDto>> GetSubcategoriesListAsync()
        {
            var data = await _context.SubCategories.Include(s => s.categoryId).Select(s => new SubListForDataGridDto()
            {
                CategoryId = s.categoryId,
                CategoryName = s.category.name,
                Id = s.id,
                IsActive = s.isActive,
                Name = s.name,
            }).ToListAsync();

            return data;
        }

        public async Task<bool> UpdateSubCategory(SubCategory model)
        {
            try
            {
                var data = GetById(model.id);
                data.isActive = model.isActive;
                data.name = model.name;
                data.categoryId = model.categoryId;
                _context.SubCategories.Attach(data);
                _context.Entry(data).State = EntityState.Modified;
                await _context.SaveChangesAsync();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
