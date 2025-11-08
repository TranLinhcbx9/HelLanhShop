using HelLanhShop.Application.ComboTemplateItems.Interfaces;
using HelLanhShop.Application.ComboTemplates.Interfaces;
using HelLanhShop.Application.Customers.Interfaces;
using HelLanhShop.Application.Employees.Interfaces;
using HelLanhShop.Application.InventoryEntries.Interfaces;
using HelLanhShop.Application.InventoryEntryDetails.Interfaces;
using HelLanhShop.Application.Products.Interfaces;
using HelLanhShop.Application.SaleDetails.Interfaces;
using HelLanhShop.Application.Sales.Interfaces;
using HelLanhShop.Application.Suppliers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Application.Common.Interfaces
{
    public interface IUnitOfWork
    {
        IComboTemplateItemRepository ComboTemplateItems { get; }
        IComboTemplateRepository ComboTemplates { get; }
        ICustomerRepository Customers { get; }
        IEmployeeRepository Employees { get; }
        IInventoryEntryDetailRepository InventoryEntryDetails { get; }
        IInventoryEntryRepository InventoryEntries { get; }
        IProductRepository Products { get; }
        ISaleDetailRepository SaleDetails { get; }
        ISaleRepository Sales { get; }
        ISupplierRepository Suppliers { get; }
        Task<int> SaveChangesAsync();

    }
}
