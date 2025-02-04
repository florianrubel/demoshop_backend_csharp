using Microsoft.Extensions.Configuration;
using SharedPropertyValueCache.Constants;
using StackExchange.Redis;
using System.Text.Json;

namespace SharedPropertyValueCache.Services
{
    public class PropertyValueCacheService : PropertyValueCacheReadOnlyService, IPropertyValueCacheService
    {
        public PropertyValueCacheService(IConfiguration configuration) : base(configuration) { }

        public async Task SetValuesForProperty(Guid propertyId, IEnumerable<string> values)
        {
            // Create a connection multiplexer
            var redis = await ConnectionMultiplexer.ConnectAsync(_connectionString);

            // Access the database
            IDatabase db = redis.GetDatabase();

            var json = JsonSerializer.Serialize(values);

            // Set a key-value pair
            await db.StringSetAsync($"{Cache.PROPERTY_VALUE_ITEM_KEY_PREFIX}{propertyId}", json);

            // Clean up
            redis.Dispose();
        }
    }
}
