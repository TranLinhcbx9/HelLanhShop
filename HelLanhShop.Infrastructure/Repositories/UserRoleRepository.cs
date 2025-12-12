using HelLanhShop.Application.UserRoles.Interfaces;
using HelLanhShop.Domain.Entities;
using HelLanhShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class UserRoleRepository : GenericRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository (HelLanhDBContext context) : base(context)
        {
        }   
    }
}
