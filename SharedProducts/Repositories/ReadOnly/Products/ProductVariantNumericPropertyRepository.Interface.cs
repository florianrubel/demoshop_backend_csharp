using Shared.Repositories;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;

namespace SharedProducts.Repositories.ReadOnly.Products
{
    public interface IProductVariantNumericPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantNumericProperty
        where SearchParametersType : ProductVariantNumericPropertyPaginationParameters
    {
    }
}