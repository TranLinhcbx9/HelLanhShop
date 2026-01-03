using FluentValidation;
using HelLanhShop.Application.Authentications.Interfaces;
using HelLanhShop.Application.Authentications.Services;
using HelLanhShop.Application.ComboTemplateItems.Interfaces;
using HelLanhShop.Application.ComboTemplates.Interfaces;
using HelLanhShop.Application.Common;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Common.Services;
using HelLanhShop.Application.Common.Validation.Base;
using HelLanhShop.Application.Customers.Interfaces;
using HelLanhShop.Application.Employees.Interfaces;
using HelLanhShop.Application.InventoryEntries.Interfaces;
using HelLanhShop.Application.InventoryEntryDetails.Interfaces;
using HelLanhShop.Application.Permissions.Interfaces;
using HelLanhShop.Application.Products.Interfaces;
using HelLanhShop.Application.Products.Services;
using HelLanhShop.Application.RefreshTokens.Interfaces;
using HelLanhShop.Application.RolePermissions.Interfaces;
using HelLanhShop.Application.Roles.Interfaces;
using HelLanhShop.Application.SaleDetails.Interfaces;
using HelLanhShop.Application.Sales.Interfaces;
using HelLanhShop.Application.Suppliers.Interfaces;
using HelLanhShop.Application.UserPermissions.Interfaces;
using HelLanhShop.Application.UserRoles.Interfaces;
using HelLanhShop.Application.Users.Interfaces;
using HelLanhShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace HelLanhShop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IComboTemplateItemRepository, ComboTemplateItemRepository>();
            services.AddScoped<IComboTemplateRepository, ComboTemplateRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IInventoryEntryDetailRepository,InventoryEntryDetailRepository>();
            services.AddScoped<IInventoryEntryRepository, InventoryEntryRepository>();
            services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IPermissionRepository, PermissionRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

            //validations
            services.AddValidatorsFromAssemblyContaining<AssemblyMarker>();

            //audit
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUser, CurrentUser>();

            return services;
        }
    }
}
