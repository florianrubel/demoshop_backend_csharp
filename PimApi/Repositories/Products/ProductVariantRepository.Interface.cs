using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantRepository<EntityType, PaginationParametersType> : IUuidBaseRepository<EntityType, PaginationParametersType>
        where EntityType : ProductVariant
        where PaginationParametersType : ProductVariantPaginationParameters
    {
    }
}