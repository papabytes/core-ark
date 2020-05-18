using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CoreArk.Packages.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreArk.Packages.Core
{
    public class DataContext : DbContext
    {
        private readonly IConfigurationAssemblyResolver _configurationAssemblyResolver;

        public DataContext(DbContextOptions<DataContext> options,
            IConfigurationAssemblyResolver configurationAssemblyResolver) : base(options)
        {
            _configurationAssemblyResolver = configurationAssemblyResolver;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.Load(_configurationAssemblyResolver.GetConfigurationAssembly()));
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Added)
                .Select(t => t.Entity);


            foreach (var entity in added)
            {
                if (entity is IAuditableEntity auditableEntity)
                {
                    auditableEntity.CreatedAt = DateTimeOffset.UtcNow;
                }

                if (entity is IEntityWithId entityWithId)
                {
                    entityWithId.Id = Guid.NewGuid();
                }

                if (entity is IEntityWithNormalizedName entityWithNormalizedName)
                {
                    entityWithNormalizedName.NormalizedName = entityWithNormalizedName.Name.ToUpperInvariant();
                }
            }

            var modified = ChangeTracker.Entries()
                .Where(t => t.State == EntityState.Modified)
                .Select(t => t.Entity);

            foreach (var entity in modified)
            {
                if (entity is IAuditableEntity auditableEntity)
                {
                    auditableEntity.UpdatedAt = DateTime.Now;
                }

                if (entity is IEntityWithNormalizedName entityWithNormalizedName)
                {
                    entityWithNormalizedName.NormalizedName = entityWithNormalizedName.Name.ToUpperInvariant();
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}