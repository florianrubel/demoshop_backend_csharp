using ContentApi.DbContexts;
using ContentApi.Services.ProductCache;
using LibContent.Entities;
using LibDb.Models.Api;
using LibUniversal.Constants;
using LibUniversal.Helpers;
using LibUniversal.Models.Api;

namespace ContentApi.Repositories
{
    public class ContentElementRepository
        : LibDb.Repositories.UuidBaseRepository<MainDbContext, ContentElement, SearchParameters>
        , IContentElementRepository<ContentElement, SearchParameters>
    {
        private readonly IContentCacheService _contentCacheService;

        public ContentElementRepository(MainDbContext context, IContentCacheService contentCacheService) : base(context)
        {
            _contentCacheService = contentCacheService;
        }

        public async override Task<ContentElement> Create(ContentElement entity)
        {
            var result = await base.Create(entity);

            await _contentCacheService.SetContentCache(result.Page);

            return result;
        }

        public async override Task<IEnumerable<ContentElement>> CreateRange(IEnumerable<ContentElement> entities)
        {
            var results = await base.CreateRange(entities);

            foreach (ContentElement result in results)
            {
                await _contentCacheService.SetContentCache(result.Page);
            }

            return results;
        }

        public async override Task<PagedList<ContentElement>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<ContentElement>;

            //if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            //{
            //    collection = collection.Where(r =>
            //        (r.Properties != null && r.Properties.Values() .Contains(parameters.SearchQuery.ToLower()))
            //}

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<ContentElement>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<ContentElement> Update(ContentElement entity, ContentElement? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            await _contentCacheService.SetContentCache(result.Page);

            return result;
        }

        public async override Task<IEnumerable<ContentElement>> UpdateRange(IEnumerable<ContentElement> entities, IDictionary<Guid, ContentElement>? oldEntities = null)
        {
            var results = await base.UpdateRange(entities, oldEntities);

            foreach (ContentElement result in results)
            {
                await _contentCacheService.SetContentCache(result.Page);
            }

            return results;
        }
    }
}
