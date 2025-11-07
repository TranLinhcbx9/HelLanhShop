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
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(HelLanhDBContext context) : base(context)
        {
        }
    }
}
