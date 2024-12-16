using ProductSearchApi.Services;

namespace ProductSearchApi.Startup
{
    public static class Services
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IProductSearchService, ProductSearchService>();
        }
    }
}
