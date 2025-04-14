using Microsoft.EntityFrameworkCore;
using LibUniversal.Entities;
using LibUniversal.Models.Api;

namespace LibDb.Repositories
{
    public abstract class UuidBaseRepository<DbContextType, EntityType, PaginationParametersType>
        : UuidReadOnlyRepository<DbContextType, EntityType, PaginationParametersType>
        , IUuidBaseRepository<EntityType, PaginationParametersType>
        where DbContextType : DbContext
        where EntityType : UuidBaseEntity, new()
        where PaginationParametersType : PaginationParameters
    {
        public UuidBaseRepository(DbContextType context) : base(context) { }

        public virtual async Task<EntityType> Create(EntityType entity)
        {
            EntityType entityProxy = _dbSet.CreateProxy();
            _dbContext.Entry(entityProxy).CurrentValues.SetValues(entity);
            await _dbSet.AddAsync(entityProxy);
            await _dbContext.SaveChangesAsync();
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
            return entityProxies;
        }

        public virtual async Task<EntityType> Update(EntityType entity, EntityType oldEntity)
        {
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<IEnumerable<EntityType>> UpdateRange(IEnumerable<EntityType> entities, IEnumerable<EntityType> oldEntities)
        {
            _dbSet.UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
            return entities;
        }

        public virtual async Task Delete(EntityType entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRange(IEnumerable<EntityType> entities)
        {
            _dbSet.RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }
        public virtual async Task Delete(Guid id)
        {
            _dbSet.Remove(new EntityType { Id = id });
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteRange(IEnumerable<Guid> ids)
        {
            _dbSet.RemoveRange(from id in ids select new EntityType { Id = id });
            await _dbContext.SaveChangesAsync();
        }
    }
}
