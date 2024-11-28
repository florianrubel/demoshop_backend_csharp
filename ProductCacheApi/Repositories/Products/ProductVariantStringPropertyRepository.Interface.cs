using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductVariantStringPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantStringProperty
        where SearchParametersType : ISearchParameters
    {
    }
}