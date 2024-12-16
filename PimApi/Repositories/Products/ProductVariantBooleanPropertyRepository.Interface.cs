using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantBooleanPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantBooleanProperty
        where SearchParametersType : ProductVariantBooleanPropertyPaginationParameters
    {
    }
}