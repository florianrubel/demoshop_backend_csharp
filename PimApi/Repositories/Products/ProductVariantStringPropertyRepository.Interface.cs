using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantStringPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantStringProperty
        where SearchParametersType : ProductVariantStringPropertySearchParameters
    {
    }
}