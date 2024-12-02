using SharedProducts.Entities.Products;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using ProductCacheApi.Repositories.Products;
using ProductCacheApi.Repositories.Products.Properties;
using Shared.Models.Api;

namespace ProductCacheApi.Startup
{
    public static class Repositories
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IBooleanPropertyRepository<BooleanProperty, ISearchParameters>, BooleanPropertyRepository>()
                .AddScoped<INumericPropertyRepository<NumericProperty, ISearchParameters>, NumericPropertyRepository>()
                .AddScoped<IStringPropertyRepository<StringProperty, ISearchParameters>, StringPropertyRepository>()
                .AddScoped<IProductRepository<Product, ISearchParameters>, ProductRepository>()
                .AddScoped<IProductVariantRepository<ProductVariant, IProductVariantPaginationParameters>, ProductVariantRepository>()
                .AddScoped<IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, IProductVariantBooleanPropertyPaginationParameters>, ProductVariantBooleanPropertyRepository>()
                .AddScoped<IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, IProductVariantNumericPropertyPaginationParameters>, ProductVariantNumericPropertyRepository>()
                .AddScoped<IProductVariantStringPropertyRepository<ProductVariantStringProperty, IProductVariantStringPropertySearchParameters>, ProductVariantStringPropertyRepository>();
        }
    }
}
