using POS_Shop.Interfaces;
using POS_Shop.Models;

namespace POS_Shop.Repositories
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(POSDbContext context) : base(context)
        {
        }
    }
}
