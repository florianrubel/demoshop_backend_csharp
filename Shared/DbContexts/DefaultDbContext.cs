using Microsoft.EntityFrameworkCore;
using Shared.Entities;

namespace Shared.DbContexts
{
    public class DefaultDbContext<DbContextType> : DbContext
        where DbContextType : DbContext
    {

        public DefaultDbContext(DbContextOptions<DbContextType> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Do not allow cascading deletes! This prevents unwanted deletions and loss of data.
            foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            // Handle added entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> createdEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in createdEntries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }

            // Handle updated entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> updatedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in updatedEntries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTimeOffset.UtcNow;
            }

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            // Handle added entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> createdEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in createdEntries)
            {
                ((BaseEntity)entityEntry.Entity).CreatedAt = DateTimeOffset.UtcNow;
            }

            // Handle updated entries
            IEnumerable<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> updatedEntries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Modified));

            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry entityEntry in updatedEntries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTimeOffset.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
