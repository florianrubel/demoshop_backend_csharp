using PimApi.Cache;

namespace PimApi.Startup
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
