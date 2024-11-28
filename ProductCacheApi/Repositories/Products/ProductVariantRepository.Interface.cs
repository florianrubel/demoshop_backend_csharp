using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductVariantRepository<EntityType, PaginationParametersType> : IUuidReadOnlyRepository<EntityType, PaginationParametersType>
        where EntityType : ProductVariant
        where PaginationParametersType : IPaginationParameters
    {
    }
}