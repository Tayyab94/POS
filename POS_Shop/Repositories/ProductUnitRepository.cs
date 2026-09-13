using POS_Shop.Interfaces;
using POS_Shop.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class ProductUnitRepository : Repository<ProductUnit>, IProductUnitRepository
    {
        public ProductUnitRepository(POSDbContext context) : base(context)
        {
        }

        public bool IsProductUnitUsedWithRecord(int pUnitId)
        {
            var isUsed =  _context.ProductPrices.Any(p => p.Prod_Unit_TypeId == pUnitId);
            return isUsed;
        }
    }
}
