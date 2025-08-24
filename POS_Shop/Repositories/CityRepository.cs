using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Repositories
{
    public class CityRepository : Repository<City>
    {
        public CityRepository(POSDbContext context) : base(context)
        {
        }
    }
}
