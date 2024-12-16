using Microsoft.Extensions.Options;
using SharedStockCache.Models.StockItemCache;
using StackExchange.Redis;

namespace SharedStockCache.Services
{
    public class StockItemCacheReadOnlyService : IStockItemCacheReadOnlyService
    {
        public readonly IOptions<RedisConnectionSettings> _connectionSettings;

        public StockItemCacheReadOnlyService(IOptions<RedisConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings;
        }

        public async Task<int> GetStockAmountForProductVariant(Guid id)
        {
            // Create a connection multiplexer
            var redis = await ConnectionMultiplexer.ConnectAsync(_connectionSettings.Value.ConnectionString);

            // Access the database
            IDatabase db = redis.GetDatabase();

            // Get the value by key
            string value = await db.StringGetAsync(id.ToString());

            // Clean up
            redis.Dispose();

            return Convert.ToInt32(value);
        }
    }
}
