using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Helpers;
using Shared.Models.Api;

namespace Shared.Repositories
{
    public abstract class UuidReadOnlyRepository<DbContextType, EntityType, PaginationParametersType> : IUuidReadOnlyRepository<EntityType, PaginationParametersType>
        where DbContextType : DbContext
        where EntityType : UuidBaseEntity
        where PaginationParametersType : PaginationParameters
    {
        protected readonly DbContextType _dbContext;
        protected readonly DbSet<EntityType> _dbSet;

        public UuidReadOnlyRepository(DbContextType context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<EntityType>();
        }

        public virtual async Task<EntityType?> GetOneOrDefault(Guid id)
        {
            EntityType? entity = await _dbSet.FindAsync(id);

            return entity;
        }

        public virtual async Task<IEnumerable<EntityType>> GetMultipleByIds(IEnumerable<Guid> ids, ShapingWithOrderingParameters? parameters = null)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            if (parameters == null)
            {
                parameters = new ShapingWithOrderingParameters();
            }

            IQueryable<EntityType> collection = _dbSet as IQueryable<EntityType>;

            collection = collection.Where(r => ids.Distinct().Contains(r.Id));

            List<EntityType> entities = await collection.ApplySort(parameters.OrderBy).ToListAsync();

            return entities;
        }

        public virtual async Task<PagedList<EntityType>> GetMultiple(PaginationParametersType parameters)
        {
            IQueryable<EntityType> collection = _dbSet as IQueryable<EntityType>;

            collection = collection.ApplySort(parameters.OrderBy);

            PagedList<EntityType> pagedList = await PagedList<EntityType>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
