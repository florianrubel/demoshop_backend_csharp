using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace SharedProducts.Repositories.Write.Products
{
    public interface IProductRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : Product
        where SearchParametersType : SearchParameters
    {
    }
}