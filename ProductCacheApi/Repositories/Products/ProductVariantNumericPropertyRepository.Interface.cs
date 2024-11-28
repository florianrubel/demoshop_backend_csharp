using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductVariantNumericPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantNumericProperty
        where SearchParametersType : IPaginationParameters
    {
    }
}