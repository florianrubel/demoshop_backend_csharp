using SharedProducts.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace SharedProducts.Repositories.ReadOnly.Products.Properties
{
    public interface IStringPropertyRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : StringProperty
        where SearchParametersType : SearchParameters
    {
    }
}