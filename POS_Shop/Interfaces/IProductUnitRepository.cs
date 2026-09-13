using POS_Shop.Models;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface IProductUnitRepository: IRepository<ProductUnit>
    {
        bool IsProductUnitUsedWithRecord(int pUnitId);
    }
}
