using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Users.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetUserAuthGraphByUserNameOrEmailAsync(string userNameOrEmail);
        Task<User?> GetByUserNameOrEmailAsync(string userNameOrEmail);
    }
}
