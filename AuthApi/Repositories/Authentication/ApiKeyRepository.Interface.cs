using AuthApi.Entities.Authentication;
using LibDb.Repositories;
using LibUniversal.Models.Api;

namespace AuthApi.Repositories.Authentication
{
    public interface IApiKeyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ApiKey
        where SearchParametersType : PaginationParameters
    {
        Task<ApiKey?> GetOneOrDefaultByToken(string token);
    }
}