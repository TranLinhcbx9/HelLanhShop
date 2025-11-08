using HelLanhShop.Application.ComboTemplateItems.Interfaces;
using HelLanhShop.Application.ComboTemplates.Interfaces;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Customers.Interfaces;
using HelLanhShop.Application.Employees.Interfaces;
using HelLanhShop.Application.InventoryEntries.Interfaces;
using HelLanhShop.Application.InventoryEntryDetails.Interfaces;
using HelLanhShop.Application.Products.Interfaces;
using HelLanhShop.Application.SaleDetails.Interfaces;
using HelLanhShop.Application.Sales.Interfaces;
using HelLanhShop.Application.Suppliers.Interfaces;
using HelLanhShop.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelLanhDBContext _context;
        public IComboTemplateItemRepository ComboTemplateItems { get; }
        public IComboTemplateRepository ComboTemplates { get; }
        public ICustomerRepository Customers { get; }
        public IEmployeeRepository Employees { get; }
        public IInventoryEntryDetailRepository InventoryEntryDetails { get; }
        public IInventoryEntryRepository InventoryEntries { get; }
        public IProductRepository Products { get; }
        public ISaleDetailRepository SaleDetails { get; }
        public ISaleRepository Sales { get; }
        public ISupplierRepository Suppliers { get; }

        public UnitOfWork (HelLanhDBContext dbcontext, 
                                        IComboTemplateItemRepository templateItemRepo, 
                                        IComboTemplateRepository comboTemplateRepo,
                                        ICustomerRepository customerRepo,
                                        IEmployeeRepository employeeRepo,
                                        IInventoryEntryDetailRepository inventoryEntryDetailRepo,
                                        IInventoryEntryRepository inventoryEntryRepo,
                                        IProductRepository productRepo,
                                        ISaleDetailRepository saleDetailRepo,
                                        ISaleRepository saleRepo,
                                        ISupplierRepository supplierRepo)
        {
            _context = dbcontext;
            ComboTemplateItems = templateItemRepo;
            ComboTemplates = comboTemplateRepo;
            Customers = customerRepo;
            Employees = employeeRepo;
            InventoryEntryDetails = inventoryEntryDetailRepo;
            InventoryEntries = inventoryEntryRepo;
            Products = productRepo;
            SaleDetails = saleDetailRepo;
            Sales = saleRepo;
            Suppliers = supplierRepo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await  _context.SaveChangesAsync();
        }
    }
}
