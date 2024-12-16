using SharedProducts.Entities.Products;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using PimApi.Repositories.Products;
using PimApi.Repositories.Products.Properties;
using Shared.Models.Api;

namespace PimApi.Startup
{
    public static class Repositories
    {
        public static void Register(WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IBooleanPropertyRepository<BooleanProperty, SearchParameters>, BooleanPropertyRepository>()
                .AddScoped<INumericPropertyRepository<NumericProperty, SearchParameters>, NumericPropertyRepository>()
                .AddScoped<IStringPropertyRepository<StringProperty, SearchParameters>, StringPropertyRepository>()
                .AddScoped<IProductRepository<Product, SearchParameters>, ProductRepository>()
                .AddScoped<IProductVariantRepository<ProductVariant, ProductVariantPaginationParameters>, ProductVariantRepository>()
                .AddScoped<IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>, ProductVariantBooleanPropertyRepository>()
                .AddScoped<IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>, ProductVariantNumericPropertyRepository>()
                .AddScoped<IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters>, ProductVariantStringPropertyRepository>();
        }
    }
}
