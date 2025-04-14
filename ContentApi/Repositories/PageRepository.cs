using ContentApi.DbContexts;
using ContentApi.Services.ProductCache;
using ContentApi.StaticServices;
using LibContent.Entities;
using LibDb.Models.Api;
using LibUniversal.Constants;
using LibUniversal.Helpers;
using LibUniversal.Models.Api;
using LibUniversal.StaticServices;

namespace ContentApi.Repositories
{
    public class PageRepository
        : LibDb.Repositories.UuidBaseRepository<MainDbContext, Page, LocalizedSearchParameters>
        , IPageRepository<Page, LocalizedSearchParameters>
    {
        private readonly IContentCacheService _contentCacheService;

        public PageRepository(MainDbContext context, IContentCacheService contentCacheService) : base(context)
        {
            _contentCacheService = contentCacheService;
        }

        public async override Task<Page> Create(Page entity)
        {
            if (entity.slug == null && !entity.LockSlug) entity.slug = PageTreeService.ResolveSlug(entity);

            var result = await base.Create(entity);

            await _contentCacheService.SetContentCache(result);

            return result;
        }

        public async override Task<IEnumerable<Page>> CreateRange(IEnumerable<Page> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.slug == null && !entity.LockSlug) entity.slug = PageTreeService.ResolveSlug(entity);
            }

            var results = await base.CreateRange(entities);

            foreach (Page result in results)
            {
                await _contentCacheService.SetContentCache(result);
            }

            return results;
        }

        public async override Task<PagedList<Page>> GetMultiple(LocalizedSearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<Page>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Title != null && r.Title.ToLower().Contains(parameters.SearchQuery.ToLower())) ||
                    (r.NavigationTitle != null && r.NavigationTitle.ToLower().Contains(parameters.SearchQuery.ToLower()))
                );
            }

            if (parameters.Parentids != null)
            {
                var parentids = TextService.GetGuidArray(parameters.Parentids);
                collection = collection.Where(r => parentids.Contains(r.Id));
            }

            if (parameters.LocalizationParentIds != null)
            {
                var localizationParentIds = TextService.GetGuidArray(parameters.LocalizationParentIds);
                collection = collection.Where(r => localizationParentIds.Contains(r.Id));
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<Page>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<Page> Update(Page entity, Page oldEntity)
        {
            if (entity.slug == null && !entity.LockSlug) entity.slug = PageTreeService.ResolveSlug(entity);

            var slugChanged = oldEntity.slug != entity.slug;
            var parentChanged = oldEntity.ParentId != entity.ParentId;
            if (slugChanged || parentChanged)
            {
                // TODO: regenerate route cache recursively downwards.
            }

            var result = await base.Update(entity, oldEntity);

            await _contentCacheService.SetContentCache(result);

            return result;
        }

        public async override Task<IEnumerable<Page>> UpdateRange(IEnumerable<Page> entities, IEnumerable<Page> oldEntities)
        {
            foreach (var entity in entities)
            {
                var oldEntity = oldEntities.Where(o => o.Id == entity.Id).FirstOrDefault();
                var slugChanged = oldEntity == null || oldEntity.slug != entity.slug;
                var parentChanged = oldEntity == null || oldEntity.ParentId != entity.ParentId;
                if (slugChanged || parentChanged)
                {
                    // TODO: regenerate route cache recursively downwards.
                }
            }

            var results = await base.UpdateRange(entities, oldEntities);

            foreach (Page result in results)
            {
                await _contentCacheService.SetContentCache(result);
            }

            return results;
        }
    }
}
