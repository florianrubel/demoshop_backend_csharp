using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;

namespace SharedProducts.Repositories.ReadOnly.Products
{
    public interface IProductVariantStringPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantStringProperty
        where SearchParametersType : ProductVariantStringPropertySearchParameters
    {
    }
}