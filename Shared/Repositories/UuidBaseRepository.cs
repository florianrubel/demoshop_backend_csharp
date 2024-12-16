using Microsoft.EntityFrameworkCore;
using Shared.Entities;
using Shared.Models.Api;

namespace Shared.Repositories
{
    public abstract class UuidBaseRepository<DbContextType, EntityType, PaginationParametersType>
        : UuidReadOnlyRepository<DbContextType, EntityType, PaginationParametersType>
        , IUuidBaseRepository<EntityType, PaginationParametersType>
        where DbContextType : DbContext
        where EntityType : UuidBaseEntity
        where PaginationParametersType : PaginationParameters
    {
        public UuidBaseRepository(DbContextType context) : base(context) { }

        public virtual async Task<EntityType> Create(EntityType entity)
        {
            EntityType entityProxy = _dbSet.CreateProxy();
            _dbContext.Entry(entityProxy).CurrentValues.SetValues(entity);
            await _dbSet.AddAsync(entityProxy);
            await _dbContext.SaveChangesAsync();
            //if (EntitiesAdded != null) EntitiesAdded.Invoke(this, new List<EntityType> { entity });
            return entityProxy;
        }

        public virtual async Task<IEnumerable<EntityType>> CreateRange(IEnumerable<EntityType> entities)
        {
            List<EntityType> entityProxies = new List<EntityType>();
            foreach (EntityType entity in entities)
            {
                EntityType entityProxy = _dbSet.CreateProxy();
                _dbContext.Entry(entityProxy).CurrentValues.SetValues(entity);
                entityProxies.Add(entityProxy);
            }
            await _dbSet.AddRangeAsync(entityProxies);
            await _dbContext.SaveChangesAsync();
            //if (EntitiesAdded != null) EntitiesAdded.Invoke(this, entityProxies);
            return entityProxies;
        }

        public virtual async Task<EntityType> Update(EntityType entity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            //if (EntitiesUpdated != null) EntitiesUpdated.Invoke(this, new List<EntityType> { entity });
            return entity;
        }

        public virtual async Task<IEnumerable<EntityType>> UpdateRange(IEnumerable<EntityType> entities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            //if (EntitiesUpdated != null) EntitiesUpdated.Invoke(this, entities);
            return entities;
        }

        public virtual async Task Delete(EntityType entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            //if (EntitiesUpdated != null) EntitiesDeleted.Invoke(this, new List<EntityType> { entity });
        }

        public virtual async Task DeleteRange(IEnumerable<EntityType> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
            //EntitiesDeleted.Invoke(this, entities);
        }
    }
}
