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
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(POSDbContext context) : base(context)
        {
        }

        public async Task<(int totalCount, IEnumerable<RolesListForDataGridDto> data)> GetRolesPagingListAsync(int pageIndex, int pageSize, string search)
        {
            var data = _context.Roles.AsQueryable();

            // apply search

            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(s => s.RoleName.Contains(search));
            }
            var totalCount = await data.CountAsync();
            var result = await data.OrderByDescending(s => s.Id)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(s => new RolesListForDataGridDto()
                {
                   Id= s.Id,
                    Name= s.RoleName
                }).ToListAsync();
            return (totalCount, result);
        }
    }
}
