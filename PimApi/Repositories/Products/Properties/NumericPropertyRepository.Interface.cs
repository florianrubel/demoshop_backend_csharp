using SharedProducts.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Repositories.Products.Properties
{
    public interface INumericPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : NumericProperty
        where SearchParametersType : SearchParameters
    {
    }
}