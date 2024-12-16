using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantNumericPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantNumericProperty
        where SearchParametersType : ProductVariantNumericPropertyPaginationParameters
    {
    }
}