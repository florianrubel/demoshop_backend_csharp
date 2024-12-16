using Shared.Entities;
using Shared.Models.Api;

namespace Shared.Repositories
{
    public interface IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : UuidBaseEntity
        where SearchParametersType : PaginationParameters
    {
        Task<EntityType?> GetOneOrDefault(Guid id);

        Task<IEnumerable<EntityType>> GetMultipleByIds(IEnumerable<Guid> ids, ShapingWithOrderingParameters? parameters = null);
        Task<PagedList<EntityType>> GetMultiple(SearchParametersType parameters);
    }
}
