using HelLanhShop.Application.Interfaces.Repositories;
using HelLanhShop.Domain.Entities;
using HelLanhShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class SaleDetailRepository : GenericRepository<SaleDetail>, ISaleDetailRepository
    {
        public SaleDetailRepository(HelLanhDBContext context) : base(context)
        {
        }
    }
}
