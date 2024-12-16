using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : Product
        where SearchParametersType : SearchParameters
    {
    }
}