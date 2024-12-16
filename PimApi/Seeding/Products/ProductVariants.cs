using SharedProducts.Entities.Products;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using PimApi.Repositories.Products;
using PimApi.Repositories.Products.Properties;
using Shared.Models.Api;
using System.Text.Json;

namespace PimApi.Seeding.Products
{
    public class ImageResult
    {
        public string url { get; set; }
    }

    public class ImagesResult
    {
        public IEnumerable<ImageResult> data { get; set; }
    }

    public static class ProductVariants
    {
        private const string CACHE_FILENAME = "cache.productVariants.json";
        private const string CACHE_FILENAME_PRODUCTVARIANT_BOOLEANPROPERTIES = "cache.productVariantsBooleanProperties.json";
        private const string CACHE_FILENAME_PRODUCTVARIANT_NUMERICPROPERTIES = "cache.productVariantsNumericProperties.json";
        private const string CACHE_FILENAME_PRODUCTVARIANT_STRINGPROPERTIES = "cache." +
            "productVariantsStringProperties.json";
        public static async Task Seed(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var writeFile = true;
                var random = new Random();
                var colors = new List<string>()
                {
                    "red",
                    "green",
                    "blue",
                    "navy",
                    "olive",
                    "black",
                    "white",
                    "grey",
                };
                var bodySizeMap = new Dictionary<string, double>()
                {
                    { "s", 170 },
                    { "m", 176 },
                    { "l", 182 },
                    { "xl", 188 },
                    { "xxl", 192 },
                    { "3xl", 192 },
                };
                var chestSizeMap = new Dictionary<string, double>()
                {
                    { "s", 90 },
                    { "m", 96 },
                    { "l", 102 },
                    { "xl", 108 },
                    { "xxl", 114 },
                    { "3xl", 120 },
                };
                var waistSizeMap = new Dictionary<string, double>()
                {
                    { "s", 85 },
                    { "m", 86 },
                    { "l", 90 },
                    { "xl", 94 },
                    { "xxl", 98 },
                    { "3xl", 102 },
                };

                var productRepository = scope.ServiceProvider.GetService<IProductRepository<Product, SearchParameters>>();
                var productVariantRepository = scope.ServiceProvider.GetService<IProductVariantRepository<ProductVariant, ProductVariantPaginationParameters>>();
                var booleanPropertyRepository = scope.ServiceProvider.GetService<IBooleanPropertyRepository<BooleanProperty, SearchParameters>>();
                var numericPropertyRepository = scope.ServiceProvider.GetService<INumericPropertyRepository<NumericProperty, SearchParameters>>();
                var stringPropertyRepository = scope.ServiceProvider.GetService<IStringPropertyRepository<StringProperty, SearchParameters>>();
                var productVariantBooleanPropertyRepository = scope.ServiceProvider.GetService<IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>>();
                var productVariantNumericPropertyRepository = scope.ServiceProvider.GetService<IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>>();
                var productVariantStringPropertyRepository = scope.ServiceProvider.GetService<IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters>>();

                var products = await productRepository.GetMultiple(new SearchParameters { PageSize = -1 });
                var booleanProperties = await booleanPropertyRepository.GetMultiple(new SearchParameters { PageSize = -1 });
                var numericProperties = await numericPropertyRepository.GetMultiple(new SearchParameters { PageSize = -1 });
                var stringProperties = await stringPropertyRepository.GetMultiple(new SearchParameters { PageSize = -1 });


                var booleanPropertyIsWaterproof = booleanProperties.Where(x => x.Name == "isWaterproof").First();
                var booleanPropertyIsNew = booleanProperties.Where(x => x.Name == "isNew").First();
                var booleanPropertyIsSale = booleanProperties.Where(x => x.Name == "isSale").First();

                var numericPropertyBodySize = numericProperties.Where(x => x.Name == "bodySize").First();
                var numericPropertyChestSize = numericProperties.Where(x => x.Name == "chestSize").First();
                var numericPropertyWaistSize = numericProperties.Where(x => x.Name == "waistSize").First();

                var stringPropertySize = stringProperties.Where(x => x.Name == "size").First();
                var stringPropertyCutout = stringProperties.Where(x => x.Name == "cutout").First();
                var stringPropertyColor = stringProperties.Where(x => x.Name == "color").First();

                var productVariantBooleanProperties = new List<ProductVariantBooleanProperty>();
                var productVariantNumericProperties = new List<ProductVariantNumericProperty>();
                var productVariantStringProperties = new List<ProductVariantStringProperty>();

                var productVariants = new List<ProductVariant>();

                if (File.Exists(CACHE_FILENAME)
                    && File.Exists(CACHE_FILENAME_PRODUCTVARIANT_BOOLEANPROPERTIES)
                    && File.Exists(CACHE_FILENAME_PRODUCTVARIANT_NUMERICPROPERTIES)
                    && File.Exists(CACHE_FILENAME_PRODUCTVARIANT_STRINGPROPERTIES))
                {
                    var json = File.ReadAllText(CACHE_FILENAME);
                    productVariants = JsonSerializer.Deserialize<List<ProductVariant>>(json);
                    await productVariantRepository.CreateRange(productVariants);

                    var jsonBoolean = File.ReadAllText(CACHE_FILENAME_PRODUCTVARIANT_BOOLEANPROPERTIES);
                    productVariantBooleanProperties = JsonSerializer.Deserialize<List<ProductVariantBooleanProperty>>(jsonBoolean);

                    var jsonNumeric = File.ReadAllText(CACHE_FILENAME_PRODUCTVARIANT_NUMERICPROPERTIES);
                    productVariantNumericProperties = JsonSerializer.Deserialize<List<ProductVariantNumericProperty>>(jsonNumeric);

                    var jsonString = File.ReadAllText(CACHE_FILENAME_PRODUCTVARIANT_STRINGPROPERTIES);
                    productVariantStringProperties = JsonSerializer.Deserialize<List<ProductVariantStringProperty>>(jsonString);
                }
                else
                {
                    var productCount = 0;
                    foreach (var product in products)
                    {
                        productCount++;
                        bool isWaterproof = random.NextDouble() >= 0.5;
                        bool isNew = random.NextDouble() >= 0.5;
                        bool isSale = random.NextDouble() >= 0.5;

                        var cutout = stringPropertyCutout.AllowedValues[random.Next(stringPropertyCutout.AllowedValues.Count)];

                        var sizeCount = 0;

                        var amountColors = random.Next(1, colors.Count);
                        var colorIndexes = new List<int>();
                        while (colorIndexes.Count < amountColors)
                        {
                            var colorIndex = random.Next(0, colors.Count - 1);
                            if (!colorIndexes.Contains(colorIndex))
                            {
                                colorIndexes.Add(colorIndex);
                            }

                        }
                        foreach (var size in stringPropertySize.AllowedValues)
                        {
                            sizeCount++;
                            var price = Convert.ToInt32(product.DefaultPriceInCents * double.Parse($"1.{sizeCount}"));

                            var colorCount = 0;
                            foreach (var colorIndex in colorIndexes)
                            {
                                colorCount++;
                                var color = colors[colorIndex];
                                var pictureUrlPrefix = $"{color}_";
                                var productVariant = new ProductVariant()
                                {
                                    PriceInCents = price,
                                    ProductId = product.Id,
                                    ListPicture = $"{pictureUrlPrefix}_0.jpg",
                                    Pictures = new List<string>(),
                                };
                                for (var i = 0; i < 5; i++)
                                {
                                    productVariant.Pictures.Add($"{pictureUrlPrefix}_{i}.webp");
                                }

                                productVariant = await productVariantRepository.Create(productVariant);
                                productVariants.Add(productVariant);

                                productVariantBooleanProperties.AddRange(new List<ProductVariantBooleanProperty>()
                                {
                                    new ProductVariantBooleanProperty { ProductVariantId = productVariant.Id, PropertyId = booleanPropertyIsWaterproof.Id, Value = isWaterproof },
                                    new ProductVariantBooleanProperty { ProductVariantId = productVariant.Id, PropertyId = booleanPropertyIsNew.Id, Value = isNew },
                                    new ProductVariantBooleanProperty { ProductVariantId = productVariant.Id, PropertyId = booleanPropertyIsSale.Id, Value = isSale },
                                });

                                    productVariantNumericProperties.AddRange(new List<ProductVariantNumericProperty>()
                                {
                                    new ProductVariantNumericProperty { ProductVariantId = productVariant.Id, PropertyId = numericPropertyBodySize.Id, Value = bodySizeMap[size] },
                                    new ProductVariantNumericProperty { ProductVariantId = productVariant.Id, PropertyId = numericPropertyChestSize.Id, Value = chestSizeMap[size] },
                                    new ProductVariantNumericProperty { ProductVariantId = productVariant.Id, PropertyId = numericPropertyWaistSize.Id, Value = waistSizeMap[size] },
                                });

                                    productVariantStringProperties.AddRange(new List<ProductVariantStringProperty>()
                                {
                                    new ProductVariantStringProperty { ProductVariantId = productVariant.Id, PropertyId = stringPropertySize.Id, Value = size },
                                    new ProductVariantStringProperty { ProductVariantId = productVariant.Id, PropertyId = stringPropertyColor.Id, Value = color },
                                    new ProductVariantStringProperty { ProductVariantId = productVariant.Id, PropertyId = stringPropertyCutout.Id, Value = cutout },
                                });
                                Console.Write($"Seeding product variants for product: {productCount} - {sizeCount} / {stringPropertySize.AllowedValues.Count} - {colorCount} / {amountColors}");
                                var position = Console.GetCursorPosition();
                                Console.SetCursorPosition(0, position.Top);
                            }
                        }
                    }
                }
                productVariantBooleanProperties = (await productVariantBooleanPropertyRepository.CreateRange(productVariantBooleanProperties)).ToList();
                productVariantNumericProperties = (await productVariantNumericPropertyRepository.CreateRange(productVariantNumericProperties)).ToList();
                productVariantStringProperties = (await productVariantStringPropertyRepository.CreateRange(productVariantStringProperties)).ToList();

                if (writeFile)
                {
                    var wJson = JsonSerializer.Serialize(productVariants);
                    File.WriteAllText(CACHE_FILENAME, wJson);

                    var wJsonBoolean = JsonSerializer.Serialize(productVariantBooleanProperties);
                    File.WriteAllText(CACHE_FILENAME_PRODUCTVARIANT_BOOLEANPROPERTIES, wJsonBoolean);

                    var wJsonNumeric = JsonSerializer.Serialize(productVariantNumericProperties);
                    File.WriteAllText(CACHE_FILENAME_PRODUCTVARIANT_NUMERICPROPERTIES, wJsonNumeric);

                    var wJsonString = JsonSerializer.Serialize(productVariantStringProperties);
                    File.WriteAllText(CACHE_FILENAME_PRODUCTVARIANT_STRINGPROPERTIES, wJsonString);
                }
                Console.WriteLine("Product Variants seeded");
            }
        }
    }
}
