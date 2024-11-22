using ProductApi.Entities.Products.Properties;
using ProductApi.Repositories.Products.Properties;
using Shared.Models.Api;

namespace ProductApi.Seeding.Products.Properties
{
    public static class StringProperties
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var repository = scope.ServiceProvider.GetService<IStringPropertyRepository<StringProperty, SearchParameters>>();

                var properties = new List<StringProperty>()
                {
                    new StringProperty { Name = "size", AllowedValues = new List<string>() { "s", "m", "l", "xl", "xxl", "3xl" } },
                    new StringProperty { Name = "cutout", AllowedValues = new List<string>() { "classic", "v", "deepv", "round", "roundtight", "u" } },
                };

                await repository.CreateRange(properties);
            }
        }
    }
}
