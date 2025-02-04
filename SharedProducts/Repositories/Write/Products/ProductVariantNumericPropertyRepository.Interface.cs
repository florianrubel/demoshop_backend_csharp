using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;

namespace SharedProducts.Repositories.Write.Products
{
    public interface IProductVariantNumericPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantNumericProperty
        where SearchParametersType : ProductVariantNumericPropertyPaginationParameters
    {
    }
}