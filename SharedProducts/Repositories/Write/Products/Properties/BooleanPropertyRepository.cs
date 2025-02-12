using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using SharedProducts.DbContexts;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products.Properties
{
    public class BooleanPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, BooleanProperty, SearchParameters>
        , IBooleanPropertyRepository<BooleanProperty, SearchParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public BooleanPropertyRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<BooleanProperty> Create(BooleanProperty entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByBooleanProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<BooleanProperty>> CreateRange(IEnumerable<BooleanProperty> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByBooleanProperties(from entity in result select entity.Id);

            return result;
        }

        public async override Task<PagedList<BooleanProperty>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<BooleanProperty>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.ToLower().Contains(parameters.SearchQuery.ToLower()))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<BooleanProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<BooleanProperty> Update(BooleanProperty entity, BooleanProperty? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByBooleanProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<BooleanProperty>> UpdateRange(IEnumerable<BooleanProperty> entities, IDictionary<Guid, BooleanProperty>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByBooleanProperties(from entity in result select entity.Id);

            return result;
        }
    }
}
