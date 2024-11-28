using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantBooleanPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantBooleanProperty
        where SearchParametersType : IPaginationParameters
    {
    }
}