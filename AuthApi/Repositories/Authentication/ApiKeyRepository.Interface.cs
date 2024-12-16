using AuthApi.Entities.Authentication;
using Shared.Models.Api;
using Shared.Repositories;

namespace AuthApi.Repositories.Authentication
{
    public interface IApiKeyRepository<EntityType, SearchParametersType> : IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : ApiKey
        where SearchParametersType : PaginationParameters
    {
        Task<ApiKey?> GetOneOrDefaultByToken(string token);
    }
}