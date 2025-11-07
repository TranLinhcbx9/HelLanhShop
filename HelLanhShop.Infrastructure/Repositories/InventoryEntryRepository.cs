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
    public class InventoryEntryRepository : GenericRepository<InventoryEntry>, IInventoryEntryRepository
    {
        public InventoryEntryRepository(HelLanhDBContext context) : base(context)
        {
        }
    }
}
