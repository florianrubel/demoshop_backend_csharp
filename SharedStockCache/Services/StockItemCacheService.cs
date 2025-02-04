using Microsoft.Extensions.Options;
using SharedStockCache.Constants;
using SharedStockCache.Models.StockItemCache;
using StackExchange.Redis;

namespace SharedStockCache.Services
{
    public class StockItemCacheService : StockItemCacheReadOnlyService, IStockItemCacheService
    {
        public StockItemCacheService(IOptions<RedisConnectionSettings> connectionSettings) : base(connectionSettings) { }

        public async Task SetStockAmountForProductVariant(Guid id, int amount)
        {
            // Create a connection multiplexer
            var redis = await ConnectionMultiplexer.ConnectAsync(_connectionSettings.Value.ConnectionString);

            // Access the database
            IDatabase db = redis.GetDatabase();

            // Set a key-value pair
            await db.StringSetAsync($"{Stock.STOCK_CACHE_ITEM_KEY_PREFIX}{id.ToString()}", amount);

            // Clean up
            redis.Dispose();
        }
    }
}
