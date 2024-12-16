using SharedProducts.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductCacheApi.Repositories.Products.Properties
{
    public interface IBooleanPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : BooleanProperty
        where SearchParametersType : SearchParameters
    {
    }
}