using ProductCacheApi.Cache;

namespace ProductCacheApi.Seeding
{
    public static class ProductCache
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var productCache = scope.ServiceProvider.GetService<IProductCacheFactory>();

                await productCache.Build();
            }
            Console.WriteLine("Caches seeded");
        }
    }
}
