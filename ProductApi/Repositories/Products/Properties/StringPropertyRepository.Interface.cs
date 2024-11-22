using ProductApi.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductApi.Repositories.Products.Properties
{
    public interface IStringPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : StringProperty
        where SearchParametersType : ISearchParameters
    {
    }
}