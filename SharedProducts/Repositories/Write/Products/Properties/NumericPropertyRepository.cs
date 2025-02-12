using SharedProducts.DbContexts;
using SharedProducts.Entities.Products.Properties;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products.Properties
{
    public class NumericPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, NumericProperty, SearchParameters>
        , INumericPropertyRepository<NumericProperty, SearchParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public NumericPropertyRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<NumericProperty> Create(NumericProperty entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByNumericProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<NumericProperty>> CreateRange(IEnumerable<NumericProperty> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByNumericProperties(from entity in result select entity.Id);

            return result;
        }

        public async override Task<PagedList<NumericProperty>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<NumericProperty>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.ToLower().Contains(parameters.SearchQuery.ToLower()))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<NumericProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<NumericProperty> Update(NumericProperty entity, NumericProperty? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByNumericProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<NumericProperty>> UpdateRange(IEnumerable<NumericProperty> entities, IDictionary<Guid, NumericProperty>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByNumericProperties(from entity in result select entity.Id);

            return result;
        }
    }
}
