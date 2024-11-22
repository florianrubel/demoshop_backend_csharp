using ProductApi.Entities.Products.Properties;
using ProductApi.Repositories.Products.Properties;

namespace ProductApi.Seeding.Products.Properties
{
    public static class NumericProperties
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<NumericPropertyRepository>();

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
