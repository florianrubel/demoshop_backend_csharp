using ProductCacheApi.Cache;

namespace ProductCacheApi.Startup
{
    public static class Caches
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IProductCacheFactory, ProductCacheFactory>();
        }
    }
}
