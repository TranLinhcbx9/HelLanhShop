using HelLanhShop.Application.ComboTemplateItems.Interfaces;
using HelLanhShop.Application.ComboTemplates.Interfaces;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Customers.Interfaces;
using HelLanhShop.Application.Employees.Interfaces;
using HelLanhShop.Application.InventoryEntries.Interfaces;
using HelLanhShop.Application.InventoryEntryDetails.Interfaces;
using HelLanhShop.Application.Permissions.Interfaces;
using HelLanhShop.Application.Products.Interfaces;
using HelLanhShop.Application.RefreshTokens.Interfaces;
using HelLanhShop.Application.RolePermissions.Interfaces;
using HelLanhShop.Application.Roles.Interfaces;
using HelLanhShop.Application.SaleDetails.Interfaces;
using HelLanhShop.Application.Sales.Interfaces;
using HelLanhShop.Application.Suppliers.Interfaces;
using HelLanhShop.Application.UserPermissions.Interfaces;
using HelLanhShop.Application.UserRoles.Interfaces;
using HelLanhShop.Application.Users.Interfaces;
using HelLanhShop.Infrastructure.Data;
using HelLanhShop.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HelLanhDBContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IDbContextTransaction? _transaction;

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
        public IUserRepository Users { get; }
        public IRefreshTokenRepository RefreshTokens { get; }
        public IRoleRepository Roles { get; }
        public IPermissionRepository Permissions { get; }
        public IUserRoleRepository UserRoles { get; }
        public IRolePermissionRepository RolePermissions { get; }
        public IUserPermissionRepository UserPermissions { get; }

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
                                        ISupplierRepository supplierRepo,
                                        IUserRepository userRepo,
                                        IRefreshTokenRepository refreshTokenRepo,
                                        IRoleRepository roleRepo,
                                        IPermissionRepository permissionRepo,
                                        IUserRoleRepository userRoleRepo,
                                        IRolePermissionRepository rolePermissionRepo,
                                        IUserPermissionRepository userPermissionRepo)
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
            Users = userRepo;
            RefreshTokens = refreshTokenRepo;
            Roles = roleRepo;
            UserRoles = userRoleRepo;
            Permissions = permissionRepo;
            UserPermissions = userPermissionRepo;
            RolePermissions = rolePermissionRepo;
        }
        public IGenericRepository<T> GenericRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.ContainsKey(type))
            {
                var repo = new GenericRepository<T>(_context);
                _repositories[type] = repo;
            }
            return (IGenericRepository<T>)_repositories[type];
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.CommitAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
                await _transaction.RollbackAsync();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await  _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
