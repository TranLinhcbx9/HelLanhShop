using HelLanhShop.Application.UserPermissions.Interfaces;
using HelLanhShop.Domain.Entities;
using HelLanhShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class UserPermissionRepository : GenericRepository<UserPermission>, IUserPermissionRepository 
    {
        public UserPermissionRepository (HelLanhDBContext context) : base(context)
        {

        }
    }
}
