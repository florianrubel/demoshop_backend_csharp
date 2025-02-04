using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace SharedProducts.Repositories.ReadOnly.Products
{
    public interface IProductRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : Product
        where SearchParametersType : SearchParameters
    {
    }
}