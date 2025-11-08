using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Employees.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
    }
}
