using SharedProducts.DbContexts;
using SharedProducts.Entities.Products.Properties;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products.Properties
{
    public class StringPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, StringProperty, SearchParameters>
        , IStringPropertyRepository<StringProperty, SearchParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public StringPropertyRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<StringProperty> Create(StringProperty entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByStringProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<StringProperty>> CreateRange(IEnumerable<StringProperty> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByStringProperties(from entity in result select entity.Id);

            return result;
        }

        public async override Task<PagedList<StringProperty>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<StringProperty>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.ToLower().Contains(parameters.SearchQuery.ToLower()))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<StringProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<StringProperty> Update(StringProperty entity, StringProperty? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByStringProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<StringProperty>> UpdateRange(IEnumerable<StringProperty> entities, IDictionary<Guid, StringProperty>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByStringProperties(from entity in result select entity.Id);

            return result;
        }
    }
}
