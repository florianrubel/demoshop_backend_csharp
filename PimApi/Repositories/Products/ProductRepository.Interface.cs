using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Repositories.Products
{
    public interface IProductRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : Product
        where SearchParametersType : SearchParameters
    {
    }
}