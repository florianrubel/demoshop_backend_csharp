using SharedProducts.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductCacheApi.Repositories.Products.Properties
{
    public interface IStringPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : StringProperty
        where SearchParametersType : SearchParameters
    {
    }
}