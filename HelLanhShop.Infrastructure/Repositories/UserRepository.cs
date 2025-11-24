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
        public Task<User?> GetByUsernameAsync(string username)
        {
            return _dbSet.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
