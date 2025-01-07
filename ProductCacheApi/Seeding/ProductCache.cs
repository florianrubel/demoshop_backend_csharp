using Algolia.Search.Clients;
using Microsoft.Extensions.Options;
using ProductCacheApi.Cache;
using ProductCacheApi.Models;

namespace ProductCacheApi.Seeding
{
    public static class ProductCache
    {
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var algoliaSettings = scope.ServiceProvider.GetService<IOptions<AlgoliaSettings>>();

                var client = new SearchClient(algoliaSettings.Value.ApplicationId, algoliaSettings.Value.WriteApiKey);

                var existsResult = await client.IndexExistsAsync(algoliaSettings.Value.IndexName);
                if (existsResult)
                {
                    var result = await client.DeleteIndexAsync(algoliaSettings.Value.IndexName);
                    await client.WaitForTaskAsync(algoliaSettings.Value.IndexName, result.TaskID);
                }

                await client.SetSettingsAsync(algoliaSettings.Value.IndexName, new Algolia.Search.Models.Search.IndexSettings
                {
                    AttributesForFaceting = new List<string>
                    {
                        "booleanProperties.isSale",
                        "booleanProperties.isNew",
                        "booleanProperties.isWaterproof",
                        "stringProperties.size",
                        "stringProperties.cutout",
                        "stringProperties.color",
                        "priceInCents"
                    },
                    SearchableAttributes = new List<string>
                    {
                        "name",
                        "description",
                        "stringProperties.color"
                    },
                    MaxValuesPerFacet = 1000,
                    MaxFacetHits = 100,
                    NumericAttributesForFiltering = new List<string>
                    {
                        "priceInCents",
                        "numericProperties.chestSize",
                        "numericProperties.bodySize",
                        "numericProperties.waistSize",
                    },
                    PaginationLimitedTo = 20000,
                    AttributeForDistinct = "productId",
                    // Do this in query time to let the user decide.
                    //Distinct = new Algolia.Search.Models.Search.Distinct(true)
                });


                //var productCache = scope.ServiceProvider.GetService<IProductCacheFactory>();

                //await productCache.Build();
            }
            Console.WriteLine("Caches seeded");
        }

        public static async Task UnSeed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var algoliaSettings = scope.ServiceProvider.GetService<IOptions<AlgoliaSettings>>();

                var client = new SearchClient(algoliaSettings.Value.ApplicationId, algoliaSettings.Value.WriteApiKey);

                var result = await client.DeleteIndexAsync(algoliaSettings.Value.IndexName);
            }
            Console.WriteLine("Index deleted");
        }
    }
}
