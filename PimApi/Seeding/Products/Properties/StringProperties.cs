using SharedProducts.Entities.Products.Properties;
using PimApi.Repositories.Products.Properties;
using Shared.Models.Api;
using System.Text.Json;

namespace PimApi.Seeding.Products.Properties
{
    public static class StringProperties
    {
        private const string CACHE_FILENAME = "cache.numericProperties.json";

        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeFile = true;
                var repository = scope.ServiceProvider.GetService<IStringPropertyRepository<StringProperty, SearchParameters>>();

                var properties = new List<StringProperty>();

                if (File.Exists(CACHE_FILENAME))
                {
                    var json = File.ReadAllText(CACHE_FILENAME);
                    properties = JsonSerializer.Deserialize<List<StringProperty>>(json);
                }
                else
                {
                    properties.AddRange(
                        new List<StringProperty>
                        {
                            new StringProperty { Name = "size", AllowedValues = new List<string>() { "s", "m", "l", "xl", "xxl", "3xl" } },
                            new StringProperty { Name = "cutout", AllowedValues = new List<string>() { "classic", "v", "deepv", "round", "roundtight", "u" } },
                            new StringProperty { Name = "color" },
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
            Console.WriteLine("String Properties seeded");
        }
    }
}
