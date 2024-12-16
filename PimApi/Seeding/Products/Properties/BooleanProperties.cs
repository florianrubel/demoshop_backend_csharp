using SharedProducts.Entities.Products.Properties;
using PimApi.Repositories.Products.Properties;
using Shared.Models.Api;
using System.Text.Json;

namespace PimApi.Seeding.Products.Properties
{
    public static class BooleanProperties
    {
        private const string CACHE_FILENAME = "cache.booleanProperties.json";

        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeFile = true;
                var repository = scope.ServiceProvider.GetService<IBooleanPropertyRepository<BooleanProperty, SearchParameters>>();

                var properties = new List<BooleanProperty>();

                if (File.Exists(CACHE_FILENAME))
                {
                    writeFile = false;
                    var json = File.ReadAllText(CACHE_FILENAME);
                    properties = JsonSerializer.Deserialize<List<BooleanProperty>>(json);
                }
                else
                {
                    properties.AddRange(
                        new List<BooleanProperty>
                        {
                            new BooleanProperty { Name = "isWaterproof" },
                            new BooleanProperty { Name = "isNew" },
                            new BooleanProperty { Name = "isSale" },
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
            Console.WriteLine("Boolean Properties seeded");
        }
    }
}
