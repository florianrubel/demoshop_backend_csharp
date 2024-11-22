using ProductApi.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace ProductApi.Repositories.Products.Properties
{
    public interface IBooleanPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : BooleanProperty
        where SearchParametersType : ISearchParameters
    {
    }
}