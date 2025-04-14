using LibContent.Entities;
using LibDb.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace ContentApi.DbContexts
{
    public class MainDbContext : DefaultDbContext<MainDbContext>
    {
        public DbSet<Page> Pages { get; set; }
        public DbSet<ContentElement> ContentElements { get; set; }
        public DbSet<ContentElementType> ContentElementTypes { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }
}
