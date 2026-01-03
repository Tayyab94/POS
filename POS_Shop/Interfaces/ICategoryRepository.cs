using POS_Shop.Models;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<bool> UpdateCategory(Category model);
    }
}
