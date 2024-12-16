using SharedStockCache.Services;

namespace StockApi.Startup
{
    public static class Services
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IStockItemCacheService, StockItemCacheService>();
        }
    }
}
