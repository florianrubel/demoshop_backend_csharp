using Microsoft.EntityFrameworkCore;
using Shared.DbContexts;
using StockApi.Entities.Stock;

namespace StockApi.DbContexts
{
    public class MainDbContext : DefaultDbContext<MainDbContext>
    {
        public DbSet<StockItem> StockItems { get; set; }

        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }
}
