using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelLanhShop.Infrastructure.Data
{
    public class HelLanhDBContextFactory : IDesignTimeDbContextFactory<HelLanhDBContext>
    {
        public HelLanhDBContext CreateDbContext(string[] args)
        {
            // Step 1: Load configuration
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // folder Infrastructure project
                .AddJsonFile("appsettings.json")               // load connection string
                .Build();

            // Step 2: Build DbContextOptions
            var optionsBuilder = new DbContextOptionsBuilder<HelLanhDBContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            // Step 3: Return DbContext instance
            return new HelLanhDBContext(optionsBuilder.Options);
        }
    }
}
