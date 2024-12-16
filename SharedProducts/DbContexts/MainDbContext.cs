using Microsoft.EntityFrameworkCore;

namespace SharedProducts.DbContexts
{
    public class MainDbContext : DbSetsDbContext<MainDbContext>
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }
}
