using ProductApi.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductApi.Repositories.Products
{
    public interface IProductRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : Product
        where SearchParametersType : ISearchParameters
    {
    }
}