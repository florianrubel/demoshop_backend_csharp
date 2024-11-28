using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantRepository<EntityType, PaginationParametersType> : IUuidBaseRepository<EntityType, PaginationParametersType>
        where EntityType : ProductVariant
        where PaginationParametersType : IPaginationParameters
    {
    }
}