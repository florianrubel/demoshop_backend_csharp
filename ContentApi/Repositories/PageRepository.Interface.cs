using LibContent.Entities;
using LibDb.Repositories;
using LibUniversal.Models.Api;

namespace ContentApi.Repositories
{
    public interface IPageRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : Page
        where SearchParametersType : SearchParameters
    {
    }
}