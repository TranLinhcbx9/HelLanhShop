using HelLanhShop.Application.ComboTemplateItems.Interfaces;
using HelLanhShop.Application.ComboTemplates.Interfaces;
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
        IUserRepository Users { get; }
        IRefreshTokenRepository RefreshTokens { get; }
        IRoleRepository Roles { get; }
        IUserRoleRepository UserRoles { get; }
        IPermissionRepository Permissions { get; }
        IUserPermissionRepository UserPermissions { get; }
        IRolePermissionRepository RolePermissions { get; }
        IGenericRepository<T> GenericRepository<T>() where T : class;
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task<int> SaveChangesAsync();

    }
}
