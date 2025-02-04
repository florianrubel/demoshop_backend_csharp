using AutoMapper;
using SharedProducts.Repositories.Write.Products.Properties;
using Shared.Models.Api;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.NumericProperty;
using SharedProducts.Profiles.Products.Properties;
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
                var mapperConfig = new MapperConfiguration(c =>
                {
                    c.AddProfile<NumericPropertyProfile>();
                });
                var mapper = mapperConfig.CreateMapper();

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
                    var viewProperties = mapper.Map<List<ViewNumericProperty>>(properties);
                    var wJson = JsonSerializer.Serialize(viewProperties);
                    File.WriteAllText(CACHE_FILENAME, wJson);
                }
            }
            Console.WriteLine("Numeric Properties seeded");
        }
    }
}
