using SharedProducts.Entities.Products.Properties;
using Shared.Models.Api;
using Shared.Repositories;

namespace SharedProducts.Repositories.Write.Products.Properties
{
    public interface IStringPropertyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : StringProperty
        where SearchParametersType : SearchParameters
    {
    }
}