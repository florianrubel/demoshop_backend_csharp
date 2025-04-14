using LibContent.Entities;
using LibDb.Repositories;
using LibUniversal.Models.Api;

namespace ContentApi.Repositories
{
    public interface IContentElementRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ContentElement
        where SearchParametersType : SearchParameters
    {
    }
}