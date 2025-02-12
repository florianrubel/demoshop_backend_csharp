using SharedProducts.Services.ProductCache;
using SharedPropertyValueCache.Services;

namespace PimApi.Startup
{
    public static class Services
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IPropertyValueCacheService, PropertyValueCacheService>()
                .AddScoped<IProductCacheService, ProductCacheService>();
        }
    }
}
