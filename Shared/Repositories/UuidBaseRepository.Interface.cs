using Shared.Entities;
using Shared.Models.Api;

namespace Shared.Repositories
{
    public interface IUuidBaseRepository<EntityType, SearchParametersType>
        where EntityType : UuidBaseEntity
        where SearchParametersType : ISearchParameters
    {
        Task<EntityType> Create(EntityType entity);
        Task<IEnumerable<EntityType>> CreateRange(IEnumerable<EntityType> entities);

        Task Update(EntityType entity);
        Task UpdateRange(IEnumerable<EntityType> entities);

        Task Delete(EntityType entity);
        Task DeleteRange(IEnumerable<EntityType> entities);

        Task<EntityType?> GetOneOrDefault(Guid id);

        Task<IEnumerable<EntityType>> GetMultiple(IEnumerable<Guid> ids, ShapingWithOrderingParameters parameters);
        Task<PagedList<EntityType>> GetMultiple(SearchParametersType parameters);
    }
}
