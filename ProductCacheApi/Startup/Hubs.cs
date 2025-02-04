using ProductCacheApi.Hubs;

namespace ProductCacheApi.Startup
{
    public static class Hubs
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IProductCacheHub, ProductCacheHub>();


            builder.Services.AddSignalR();
        }
        public static void PostBuild(WebApplication app)
        {
            app.MapHub<ProductCacheHub>("hubs/product-cache");
        }
    }
}
