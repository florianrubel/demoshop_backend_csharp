using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantNumericPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantNumericProperty
        where SearchParametersType : IPaginationParameters
    {
    }
}