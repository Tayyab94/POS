using POS_Shop.Interfaces;
using POS_Shop.Models;

namespace POS_Shop.Repositories
{
    public class ProductUnitRepository : Repository<ProductUnit>, IProductUnitRepository
    {
        public ProductUnitRepository(POSDbContext context) : base(context)
        {
        }
    }
}
