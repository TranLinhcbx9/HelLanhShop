using HelLanhShop.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelLanhShop.Domain.Entities;

namespace HelLanhShop.Application.Permissions.Interfaces
{
    public interface IPermissionRepository : IGenericRepository<Permission>
    {
    }
}
