using MongoDB.Driver;
using SharedProducts.Entities.Products;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using ProductCacheApi.Repositories.Products;
using ProductCacheApi.Repositories.Products.Properties;
using Shared.Models.Api;
using Microsoft.Extensions.Options;
using ProductCacheApi.Models;

namespace ProductCacheApi.Cache
{
    public class ProductCacheFactory : IProductCacheFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<AlgoliaSettings> _algoliaSettings;
        private readonly IMongoClient _mongoClient;
        private readonly IBooleanPropertyRepository<BooleanProperty, ISearchParameters> _booleanPropertyRepository;
        private readonly INumericPropertyRepository<NumericProperty, ISearchParameters> _numericPropertyRepository;
        private readonly IStringPropertyRepository<StringProperty, ISearchParameters> _stringPropertyRepository;
        private readonly IProductRepository<Product, ISearchParameters> _productRepository;
        private readonly IProductVariantRepository<ProductVariant, IProductVariantSearchParameters> _productVariantRepository;
        private readonly IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, IProductVariantBooleanPropertyPaginationParameters> _productVariantBooleanPropertyRepository;
        private readonly IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, IProductVariantNumericPropertyPaginationParameters> _productVariantNumericPropertyRepository;
        private readonly IProductVariantStringPropertyRepository<ProductVariantStringProperty, IProductVariantStringPropertySearchParameters> _productVariantStringPropertyRepository;

        private readonly SearchParameters defaultSearchParameters = new SearchParameters { PageSize = -1 };

        public ProductCacheFactory(
            IConfiguration configuration,
            IOptions<AlgoliaSettings> algoliaSettings,
            IBooleanPropertyRepository<BooleanProperty, ISearchParameters> booleanPropertyRepository,
            INumericPropertyRepository<NumericProperty, ISearchParameters> numericPropertyRepository,
            IStringPropertyRepository<StringProperty, ISearchParameters> stringPropertyRepository,
            IProductRepository<Product, ISearchParameters> productRepository,
            IProductVariantRepository<ProductVariant, IProductVariantSearchParameters> productVariantRepository,
            IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, IProductVariantBooleanPropertyPaginationParameters> productVariantBooleanPropertyRepository,
            IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, IProductVariantNumericPropertyPaginationParameters> productVariantNumericPropertyRepository,
            IProductVariantStringPropertyRepository<ProductVariantStringProperty, IProductVariantStringPropertySearchParameters> productVariantStringPropertyRepository
        )
        {
            _configuration = configuration;
            _algoliaSettings = algoliaSettings;
            _booleanPropertyRepository = booleanPropertyRepository;
            _numericPropertyRepository = numericPropertyRepository;
            _stringPropertyRepository = stringPropertyRepository;
            _productRepository = productRepository;
            _productVariantRepository = productVariantRepository;
            _productVariantBooleanPropertyRepository = productVariantBooleanPropertyRepository;
            _productVariantNumericPropertyRepository = productVariantNumericPropertyRepository;
            _productVariantStringPropertyRepository = productVariantStringPropertyRepository;

            var connectionString = _configuration.GetConnectionString("ProductCache");
            _mongoClient = new MongoClient(connectionString);
        }

        public async Task Build()
        {
            await BuildCache(
                await _booleanPropertyRepository.GetMultiple(defaultSearchParameters),
                await _numericPropertyRepository.GetMultiple(defaultSearchParameters),
                await _stringPropertyRepository.GetMultiple(defaultSearchParameters),
                await _productRepository.GetMultiple(defaultSearchParameters),
                await _productVariantRepository.GetMultiple(new ProductVariantSearchParameters { PageSize = -1 }),
                await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { PageSize = -1 }),
                await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { PageSize = -1 }),
                await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { PageSize = -1 })
            );
        }

        public async Task BuildByProduct(Guid productId)
        {
            await BuildByProducts(new List<Guid> { productId });
        }
        public async Task BuildByProducts(IEnumerable<Guid> productIds)
        {
            var products = await _productRepository.GetMultipleByIds(productIds);
            var productIdsString = String.Join(',', (from product in products select product.Id.ToString()).Distinct());
            var productVariants = await _productVariantRepository.GetMultiple(new ProductVariantSearchParameters { ProductIds = productIdsString, PageSize = -1 });
            var productVariantIdsString = String.Join(',', (from productVariant in productVariants select productVariant.Id.ToString()).Distinct());
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds((from property in productVariantBooleanProperties select property.Id).Distinct());
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds((from property in productVariantNumericProperties select property.Id).Distinct());
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByProductVariant(Guid productVariantId)
        {
            await BuildByProductVariants(new List<Guid> { productVariantId });
        }

        public async Task BuildByProductVariants(IEnumerable<Guid> productVariantIds)
        {
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var products = (await _productRepository.GetMultipleByIds(from productVariant in productVariants select productVariant.ProductId)).Distinct();
            var productVariantIdsString = String.Join(',', (from productVariant in productVariants select productVariant.Id.ToString()).Distinct());
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds((from property in productVariantBooleanProperties select property.Id).Distinct());
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds((from property in productVariantNumericProperties select property.Id).Distinct());
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByBooleanProperty(Guid booleanPropertyId)
        {
            await BuildByBooleanProperties(new List<Guid> { booleanPropertyId });
        }

        public async Task BuildByBooleanProperties(IEnumerable<Guid> booleanPropertyIds)
        {
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds(booleanPropertyIds);
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultipleByIds((from property in booleanProperties select property.Id).Distinct());
            var productVariantIds = (from property in productVariantBooleanProperties select property.ProductVariantId).Distinct();
            var productVariantIdsString = String.Join(',', productVariantIds);
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var productIds = (from variant in productVariants select variant.Id).Distinct();
            var products = (await _productRepository.GetMultipleByIds(productIds)).Distinct();
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds((from property in productVariantNumericProperties select property.Id).Distinct());
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByNumericProperty(Guid numericPropertyId)
        {
            await BuildByNumericProperties(new List<Guid> { numericPropertyId });
        }

        public async Task BuildByNumericProperties(IEnumerable<Guid> numericPropertyIds)
        {
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds(numericPropertyIds);
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultipleByIds((from property in numericProperties select property.Id).Distinct());
            var productVariantIds = (from property in productVariantNumericProperties select property.ProductVariantId).Distinct();
            var productVariantIdsString = String.Join(',', productVariantIds);
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var productIds = (from variant in productVariants select variant.Id).Distinct();
            var products = (await _productRepository.GetMultipleByIds(productIds)).Distinct();
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds((from property in productVariantBooleanProperties select property.Id).Distinct());
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByStringProperty(Guid stringPropertyId)
        {
            await BuildByStringProperties(new List<Guid> { stringPropertyId });
        }

        public async Task BuildByStringProperties(IEnumerable<Guid> stringPropertyIds)
        {
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds(stringPropertyIds);
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultipleByIds((from property in stringProperties select property.Id).Distinct());
            var productVariantIds = (from property in productVariantStringProperties select property.ProductVariantId).Distinct();
            var productVariantIdsString = String.Join(',', productVariantIds);
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var productIds = (from variant in productVariants select variant.Id).Distinct();
            var products = (await _productRepository.GetMultipleByIds(productIds)).Distinct();
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds((from property in productVariantBooleanProperties select property.Id).Distinct());
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds((from property in productVariantNumericProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByProductVariantBooleanProperty(Guid productVariantBooleanPropertyId)
        {
            await BuildByProductVariantBooleanProperties(new List<Guid> { productVariantBooleanPropertyId });
        }

        public async Task BuildByProductVariantBooleanProperties(IEnumerable<Guid> productVariantBooleanPropertyIds)
        {
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultipleByIds(productVariantBooleanPropertyIds);
            var productVariantIds = (from property in productVariantBooleanProperties select property.ProductVariantId).Distinct();
            var productVariantIdsString = String.Join(',', productVariantIds);
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var productIds = (from productVariant in productVariants select productVariant.ProductId).Distinct();
            var products = await _productRepository.GetMultipleByIds(productIds);
            var booleanPropertiesIds = (from property in productVariantBooleanProperties select property.PropertyId).Distinct();
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds(booleanPropertiesIds);
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds((from property in productVariantNumericProperties select property.Id).Distinct());
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByProductVariantNumericProperty(Guid productVariantNumericPropertyId)
        {
            await BuildByProductVariantNumericProperties(new List<Guid> { productVariantNumericPropertyId });
        }

        public async Task BuildByProductVariantNumericProperties(IEnumerable<Guid> productVariantNumericPropertyIds)
        {
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultipleByIds(productVariantNumericPropertyIds);
            var productVariantIds = (from property in productVariantNumericProperties select property.ProductVariantId).Distinct();
            var productVariantIdsString = String.Join(',', productVariantIds);
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var productIds = (from productVariant in productVariants select productVariant.ProductId).Distinct();
            var products = await _productRepository.GetMultipleByIds(productIds);
            var numericPropertiesIds = (from property in productVariantNumericProperties select property.PropertyId).Distinct();
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds(numericPropertiesIds);
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultiple(new ProductVariantStringPropertySearchParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds((from property in productVariantBooleanProperties select property.Id).Distinct());
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        public async Task BuildByProductVariantStringProperty(Guid productVariantStringPropertyId)
        {
            await BuildByProductVariantStringProperties(new List<Guid> { productVariantStringPropertyId });
        }

        public async Task BuildByProductVariantStringProperties(IEnumerable<Guid> productVariantStringPropertyIds)
        {
            var productVariantStringProperties = await _productVariantStringPropertyRepository.GetMultipleByIds(productVariantStringPropertyIds);
            var productVariantIds = (from property in productVariantStringProperties select property.ProductVariantId).Distinct();
            var productVariantIdsString = String.Join(',', productVariantIds);
            var productVariants = await _productVariantRepository.GetMultipleByIds(productVariantIds);
            var productIds = (from productVariant in productVariants select productVariant.ProductId).Distinct();
            var products = await _productRepository.GetMultipleByIds(productIds);
            var stringPropertiesIds = (from property in productVariantStringProperties select property.PropertyId).Distinct();
            var stringProperties = await _stringPropertyRepository.GetMultipleByIds(stringPropertiesIds);
            var productVariantBooleanProperties = await _productVariantBooleanPropertyRepository.GetMultiple(new ProductVariantBooleanPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var productVariantNumericProperties = await _productVariantNumericPropertyRepository.GetMultiple(new ProductVariantNumericPropertyPaginationParameters { ProductVariantIds = productVariantIdsString, PageSize = -1 });
            var booleanProperties = await _booleanPropertyRepository.GetMultipleByIds((from property in productVariantBooleanProperties select property.Id).Distinct());
            var numericProperties = await _numericPropertyRepository.GetMultipleByIds((from property in productVariantStringProperties select property.Id).Distinct());

            await BuildCache(
                booleanProperties,
                numericProperties,
                stringProperties,
                products,
                productVariants,
                productVariantBooleanProperties,
                productVariantNumericProperties,
                productVariantStringProperties
            );
        }

        private async Task BuildCache(
            IEnumerable<BooleanProperty> booleanProperties,
            IEnumerable<NumericProperty> numericProperties,
            IEnumerable<StringProperty> stringProperties,
            IEnumerable<Product> products,
            IEnumerable<ProductVariant> productVariants,
            IEnumerable<ProductVariantBooleanProperty> productVariantBooleanProperties,
            IEnumerable<ProductVariantNumericProperty> productVariantNumericProperties,
            IEnumerable<ProductVariantStringProperty> productVariantStringProperties
        )
        {
            var collectionName = "products";
            var db = _mongoClient.GetDatabase("productCache");

            if (!(await db.ListCollectionNamesAsync()).ToList().Any(x => x == collectionName))
            {
                await db.CreateCollectionAsync(collectionName);
            }

            var collection = db.GetCollection<ProductCacheItem>(collectionName);

            var counter = 0;
            foreach (var productVariant in productVariants)
            {
                var product = products.FirstOrDefault(x => x.Id == productVariant.ProductId);
                if (product == null) continue;

                var item = new ProductCacheItem
                {
                    Id = productVariant.Id,
                    ProductId = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ListPicture = product.ListPicture,
                    Pictures = product.Pictures,
                    PriceInCents = productVariant.PriceInCents
                };

                foreach (var productVariantBooleanProperty in productVariantBooleanProperties.Where(x => x.ProductVariantId == productVariant.Id))
                {
                    var property = booleanProperties.FirstOrDefault(x => x.Id == productVariantBooleanProperty.PropertyId);
                    if (property == null) continue;
                    item.BooleanProperties.Add(property.Name, productVariantBooleanProperty.Value);
                }
                foreach (var productVariantNumericProperty in productVariantNumericProperties.Where(x => x.ProductVariantId == productVariant.Id))
                {
                    var property = numericProperties.FirstOrDefault(x => x.Id == productVariantNumericProperty.PropertyId);
                    if (property == null) continue;
                    item.NumericProperties.Add(property.Name, productVariantNumericProperty.Value);
                }
                foreach (var productVariantStringProperty in productVariantStringProperties.Where(x => x.ProductVariantId == productVariant.Id))
                {
                    var property = stringProperties.FirstOrDefault(x => x.Id == productVariantStringProperty.PropertyId);
                    if (property == null) continue;
                    item.StringProperties.Add(property.Name, productVariantStringProperty.Value);
                }

                await collection.ReplaceOneAsync(
                    Builders<ProductCacheItem>.Filter.Eq(x => x.Id, item.Id),
                    item,
                    new ReplaceOptions
                    {
                        IsUpsert = true,
                    }
                );

                counter++;

                Console.WriteLine($"{counter} / {productVariants.Count()} / {Math.Round((double)(counter * 100 / productVariants.Count()))}");
            }
        }
    }
}
