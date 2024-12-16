using Microsoft.EntityFrameworkCore;
using Shared.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Entities.Products.Properties;

namespace SharedProducts.DbContexts
{
    public class DbSetsDbContext<DbContextType> : DefaultDbContext<DbContextType>
        where DbContextType : DbContext
    {
        public DbSet<BooleanProperty> BooleanProperties { get; set; }
        public DbSet<NumericProperty> NumericProperties { get; set; }
        public DbSet<StringProperty> StringProperties { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductVariantBooleanProperty> ProductVariantBooleanProperties { get; set; }
        public DbSet<ProductVariantNumericProperty> ProductVariantNumericProperties { get; set; }
        public DbSet<ProductVariantStringProperty> ProductVariantStringProperties { get; set; }

        public DbSetsDbContext(DbContextOptions<DbContextType> options) : base(options)
        {
        }
    }
}
