using HelLanhShop.Domain.Common;
using HelLanhShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace HelLanhShop.Infrastructure.Data
{
    public class HelLanhDBContext :DbContext
    {
        public HelLanhDBContext(DbContextOptions<HelLanhDBContext> options) : base(options)
        {
        }

        // DbSet các bảng
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ComboTemplate> ComboTemplates { get; set; }
        public virtual DbSet<ComboTemplateItem> ComboTemplateItems { get; set; }
        public virtual DbSet<InventoryEntry> InventoryEntries { get; set; }
        public virtual DbSet<Sale> Sales { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //// Soft delete global filter
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
            //    {
            //        var parameter = Expression.Parameter(entityType.ClrType, "e");

            //        // EF.Property<bool>(e, "IsDeleted")
            //        var isDeletedProperty = Expression.Call(
            //            typeof(EF),
            //            nameof(EF.Property),
            //            new Type[] { typeof(bool) },
            //            parameter,
            //            Expression.Constant("IsDeleted")
            //        );

            //        // !EF.Property<bool>(e, "IsDeleted")
            //        var body = Expression.Not(isDeletedProperty);

            //        // Lambda
            //        var lambda = Expression.Lambda(body, parameter);

            //        // Apply query filter
            //        modelBuilder.Entity(entityType.ClrType)
            //                    .HasQueryFilter(lambda);
            //    }
            //}

            // FK tự nhận nhờ virtual + navigation property
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
