using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductVariantStringPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantStringProperty
        where SearchParametersType : ProductVariantStringPropertySearchParameters
    {
    }
}