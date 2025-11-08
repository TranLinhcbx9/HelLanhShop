using HelLanhShop.Application.Employees.Interfaces;
using HelLanhShop.Domain.Entities;
using HelLanhShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(HelLanhDBContext context) : base(context)
        {
        }
    }
}
