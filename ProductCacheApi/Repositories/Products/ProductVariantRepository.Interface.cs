using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;

namespace ProductCacheApi.Repositories.Products
{
    public interface IProductVariantRepository<EntityType, PaginationParametersType> : IUuidReadOnlyRepository<EntityType, PaginationParametersType>
        where EntityType : ProductVariant
        where PaginationParametersType : ProductVariantPaginationParameters
    {
    }
}