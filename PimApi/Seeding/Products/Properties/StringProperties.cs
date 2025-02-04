using AutoMapper;
using SharedProducts.Repositories.Write.Products.Properties;
using Shared.Models.Api;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.StringProperty;
using SharedProducts.Profiles.Products.Properties;
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
                var mapperConfig = new MapperConfiguration(c =>
                {
                    c.AddProfile<StringPropertyProfile>();
                });
                var mapper = mapperConfig.CreateMapper();

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
                    var viewProperties = mapper.Map<List<ViewStringProperty>>(properties);
                    var wJson = JsonSerializer.Serialize(viewProperties);
                    File.WriteAllText(CACHE_FILENAME, wJson);
                }
            }
            Console.WriteLine("String Properties seeded");
        }
    }
}
