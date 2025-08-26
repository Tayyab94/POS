using POS_Shop.Models;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<bool> UpdateCountry(Country model);
    }
}
