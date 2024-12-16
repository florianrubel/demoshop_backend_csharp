using AuthApi.DbContexts;
using AuthApi.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Api;

namespace AuthApi.Repositories.Authentication
{
    public class ApiKeyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ApiKey, PaginationParameters>
        , IApiKeyRepository<ApiKey, PaginationParameters>
    {
        public ApiKeyRepository(MainDbContext context) : base(context) { }

        public async Task<ApiKey?> GetOneOrDefaultByToken(string token)
        {
            var collection = _dbSet as IQueryable<ApiKey>;

            collection = collection.Where(r =>
                r.Key != null && r.Key == token
            );

            return (await collection.ToListAsync()).FirstOrDefault();
        }
    }
}
