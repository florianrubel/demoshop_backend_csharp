using Microsoft.EntityFrameworkCore;
using ProductApi.Entities.Products;
using ProductApi.Entities.Products.Properties;
using Shared.Entities;

namespace ProductApi.DbContexts
{
    public class MainDbContext : DbContext
    {
        public DbSet<BooleanProperty> BooleanProperties { get; set; }
        public DbSet<NumericProperty> NumericProperties { get; set; }
        public DbSet<StringProperty> StringhProperties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantBooleanProperty> ProductVariantBooleanProperties { get; set; }
        public DbSet<ProductVariantNumericProperty> ProductVariantNumericProperties { get; set; }
        public DbSet<ProductVariantStringProperty> ProductVariantStringProperties { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "ProductApi");
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
