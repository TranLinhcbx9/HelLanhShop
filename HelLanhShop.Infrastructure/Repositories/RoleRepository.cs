using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Roles.Interfaces;
using HelLanhShop.Domain.Entities;
using HelLanhShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(HelLanhDBContext context) : base(context)
        {
        }
        public async Task<Role?> GetByNameAsync(string roleName)
        {             
            return await _dbSet.FirstOrDefaultAsync(r => r.Name == roleName); 
        }
    }
}
