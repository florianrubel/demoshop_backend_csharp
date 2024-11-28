using SharedProducts.Entities.Products;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Repositories.Products
{
    public interface IProductVariantStringPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ProductVariantStringProperty
        where SearchParametersType : ISearchParameters
    {
    }
}