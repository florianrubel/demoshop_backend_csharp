using ProductApi.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductApi.Repositories.Products
{
    public interface IProductVariantRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariant
        where SearchParametersType : ISearchParameters
    {
    }
}