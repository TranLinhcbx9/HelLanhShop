using HelLanhShop.Application.Users.Interfaces;
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
    public class UserRepository: GenericRepository<User>, IUserRepository
    {
        public UserRepository(HelLanhDBContext context) : base(context)
        {
        }
        public async Task<User?> GetByUserNameOrEmailAsync(string userNameOrEmail)
        {
            return await _dbSet
                        .FirstOrDefaultAsync(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail);
        }

        public async Task<User?> GetUserAuthGraphByUserNameOrEmailAsync(string userNameOrEmail)
        {
            return await _dbSet
                        .Include(u => u.UserRoles)
                            .ThenInclude(ur => ur.Role)
                                .ThenInclude(r => r.RolePermissions)
                                    .ThenInclude(rp => rp.Permission)
                        .Include(u => u.UserPermissions)
                            .ThenInclude(up => up.Permission)
                        .FirstOrDefaultAsync(u => u.UserName == userNameOrEmail || u.Email == userNameOrEmail);
        }

    }
}
