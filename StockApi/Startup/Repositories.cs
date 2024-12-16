using StockApi.Entities.Stock;
using StockApi.Models.Stock.StockItem;
using StockApi.Repositories.Stock;

namespace StockApi.Startup
{
    public static class Repositories
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IStockItemRepository<StockItem, StockItemPaginationParameters>, StockItemRepository>();
        }
    }
}
