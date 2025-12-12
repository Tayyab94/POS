using POS_Shop.DTOs.City;
using POS_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Shop.Interfaces
{
    public interface IRoleRepository:IRepository<Role>
    {
        Task<(int totalCount, IEnumerable<RolesListForDataGridDto> data)> GetRolesPagingListAsync(int pageIndex, int pageSize, string search);
    }
}
