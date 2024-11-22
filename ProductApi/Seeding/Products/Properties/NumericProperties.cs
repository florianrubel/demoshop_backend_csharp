using Microsoft.Extensions.DependencyInjection;
using ProductApi.Entities.Products.Properties;
using ProductApi.Repositories.Products.Properties;
using Shared.Models.Api;

namespace ProductApi.Seeding.Products.Properties
{
    public static class NumericProperties
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<INumericPropertyRepository<NumericProperty, SearchParameters>>();

                var properties = new List<NumericProperty>()
                {
                    new NumericProperty { Name = "bodySize" },
                    new NumericProperty { Name = "chestSize" },
                    new NumericProperty { Name = "waistSize" },
                };

                await repository.CreateRange(properties);
            }
        }
    }
}
