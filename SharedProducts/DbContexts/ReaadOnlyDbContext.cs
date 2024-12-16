using Microsoft.EntityFrameworkCore;

namespace SharedProducts.DbContexts
{
    public class ReadOnlyDbContext : DbSetsDbContext<ReadOnlyDbContext>
    {
        public ReadOnlyDbContext(DbContextOptions<ReadOnlyDbContext> options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
