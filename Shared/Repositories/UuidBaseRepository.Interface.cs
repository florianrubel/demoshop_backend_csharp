using Shared.Entities;
using Shared.Models.Api;

namespace Shared.Repositories
{
    public interface IUuidBaseRepository<EntityType, SearchParametersType> : IUuidReadOnlyRepository<EntityType, SearchParametersType>
        where EntityType : UuidBaseEntity
        where SearchParametersType : PaginationParameters
    {

        Task<EntityType> Create(EntityType entity);
        Task<IEnumerable<EntityType>> CreateRange(IEnumerable<EntityType> entities);

        Task<EntityType> Update(EntityType entity);
        Task<IEnumerable<EntityType>> UpdateRange(IEnumerable<EntityType> entities);

        Task Delete(EntityType entity);
        Task DeleteRange(IEnumerable<EntityType> entities);
    }
}
