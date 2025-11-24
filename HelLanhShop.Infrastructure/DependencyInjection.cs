using HelLanhShop.Application.Authentications.Interfaces;
using HelLanhShop.Application.Authentications.Services;
using HelLanhShop.Application.ComboTemplateItems.Interfaces;
using HelLanhShop.Application.ComboTemplates.Interfaces;
using HelLanhShop.Application.Common.Interfaces;
using HelLanhShop.Application.Common.Services;
using HelLanhShop.Application.Customers.Interfaces;
using HelLanhShop.Application.Employees.Interfaces;
using HelLanhShop.Application.InventoryEntries.Interfaces;
using HelLanhShop.Application.InventoryEntryDetails.Interfaces;
using HelLanhShop.Application.Products.Interfaces;
using HelLanhShop.Application.Products.Services;
using HelLanhShop.Application.RefreshTokens.Interfaces;
using HelLanhShop.Application.SaleDetails.Interfaces;
using HelLanhShop.Application.Sales.Interfaces;
using HelLanhShop.Application.Suppliers.Interfaces;
using HelLanhShop.Application.Users.Interfaces;
using HelLanhShop.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //services
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtTokenService, JwtTokenService>();


            return services;
        }
    }
}
