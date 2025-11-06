using HelLanhShop.Application.Interfaces.Repositories;
using HelLanhShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Repositories
{
    public class ComboTemplateItemRepository : GenericRepository<HelLanhShop.Domain.Entities.ComboTemplateItem>, IComboTemplateItemRepository
    {
        public ComboTemplateItemRepository(HelLanhDBContext context) : base(context)
        {

        }
    }
}
