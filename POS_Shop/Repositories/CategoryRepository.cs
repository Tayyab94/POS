using POS_Shop.Interfaces;
using POS_Shop.Models;
using System.Linq;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(POSDbContext context) : base(context)
        {
        }

        public Task<bool> UpdateCategory(Category model)
        {
            try
            {
                var data = _context.Categories.FirstOrDefault(c => c.id == model.id);
                if (data != null)
                {
                    data.name = model.name;
                    data.isActive = model.isActive;

                    _context.Categories.Attach(data);
                    _context.Entry(data).State = System.Data.Entity.EntityState.Modified;
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
                else
                {
                    return Task.FromResult(false);
                }
            }
            catch (System.Exception)
            {
                return Task.FromResult(false);

            }
        }
    }
}
