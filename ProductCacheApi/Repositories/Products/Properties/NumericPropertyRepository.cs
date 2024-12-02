using SharedProducts.DbContexts;
using SharedProducts.Entities.Products.Properties;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;

namespace ProductCacheApi.Repositories.Products.Properties
{
    public class NumericPropertyRepository
        : Shared.Repositories.UuidReadOnlyRepository<ReadOnlyDbContext, NumericProperty, ISearchParameters>
        , INumericPropertyRepository<NumericProperty, ISearchParameters>
    {
        public NumericPropertyRepository(ReadOnlyDbContext context) : base(context) { }

        public async override Task<PagedList<NumericProperty>> GetMultiple(ISearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<NumericProperty>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.Contains(parameters.SearchQuery))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<NumericProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
