using ProductApi.Entities.Products.Properties;
using ProductApi.Repositories.Products.Properties;
using Shared.Models.Api;

namespace ProductApi.Startup
{
    public static class Repositories
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IBooleanPropertyRepository<BooleanProperty, SearchParameters>, BooleanPropertyRepository>();
        }
    }
}
