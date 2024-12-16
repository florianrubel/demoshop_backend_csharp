using SharedProducts.Entities.Products.Properties;
using PimApi.Repositories.Products.Properties;
using Shared.Models.Api;
using System.Text.Json;

namespace PimApi.Seeding.Products.Properties
{
    public static class NumericProperties
    {
        private const string CACHE_FILENAME = "cache.stringProperties.json";

        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeFile = true;
                var repository = scope.ServiceProvider.GetService<INumericPropertyRepository<NumericProperty, SearchParameters>>();

                var properties = new List<NumericProperty>();

                if (File.Exists(CACHE_FILENAME))
                {
                    var json = File.ReadAllText(CACHE_FILENAME);
                    properties = JsonSerializer.Deserialize<List<NumericProperty>>(json);
                }
                else
                {
                    properties.AddRange(
                        new List<NumericProperty>
                        {
                            new NumericProperty { Name = "bodySize" },
                            new NumericProperty { Name = "chestSize" },
                            new NumericProperty { Name = "waistSize" },
                        }
                    );
                }

                properties = (await repository.CreateRange(properties)).ToList();

                if (writeFile)
                {
                    var wJson = JsonSerializer.Serialize(properties);
                    File.WriteAllText(CACHE_FILENAME, wJson);
                }
            }
            Console.WriteLine("Numeric Properties seeded");
        }
    }
}
