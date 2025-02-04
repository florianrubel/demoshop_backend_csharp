using Microsoft.Extensions.Configuration;
using SharedPropertyValueCache.Constants;
using StackExchange.Redis;
using System.Text.Json;

namespace SharedPropertyValueCache.Services
{
    public class PropertyValueCacheReadOnlyService : IPropertyValueCacheReadOnlyService
    {
        public readonly string _connectionString;

        public PropertyValueCacheReadOnlyService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RedisConnection");
        }

        public async Task<List<string>> GetValuesForProperty(Guid propertyId)
        {
            // Create a connection multiplexer
            var redis = await ConnectionMultiplexer.ConnectAsync(_connectionString);

            // Access the database
            IDatabase db = redis.GetDatabase();

            // Get the value by key
            string value = await db.StringGetAsync($"{Cache.PROPERTY_VALUE_ITEM_KEY_PREFIX}{propertyId}");

            // Clean up
            redis.Dispose();

            if (value != null)
            {
                return JsonSerializer.Deserialize<List<string>>(value) as List<string>;
            }
            return new List<string>();
        }
    }
}
