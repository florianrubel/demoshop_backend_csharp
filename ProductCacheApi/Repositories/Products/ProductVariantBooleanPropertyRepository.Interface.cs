using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductVariantBooleanPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantBooleanProperty
        where SearchParametersType : ProductVariantBooleanPropertyPaginationParameters
    {
    }
}