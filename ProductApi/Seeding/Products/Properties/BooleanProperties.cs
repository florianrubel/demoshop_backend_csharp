using ProductApi.Entities.Products.Properties;
using ProductApi.Repositories.Products.Properties;

namespace ProductApi.Seeding.Products.Properties
{
    public static class BooleanProperties
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<BooleanPropertyRepository>();

                var properties = new List<BooleanProperty>()
                {
                    new BooleanProperty { Name = "isWaterproof" },
                    new BooleanProperty { Name = "isNew" },
                    new BooleanProperty { Name = "isSecondHand" },
                };

                await repository.CreateRange(properties);
            }
        }
    }
}
