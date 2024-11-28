using SharedProducts.DbContexts;
using SharedProducts.Entities.Products.Properties;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;

namespace ProductCacheApi.Repositories.Products.Properties
{
    public class BooleanPropertyRepository
        : Shared.Repositories.UuidReadOnlyRepository<ReadOnlyDbContext, BooleanProperty, SearchParameters>
        , IBooleanPropertyRepository<BooleanProperty, SearchParameters>
    {
        public BooleanPropertyRepository(ReadOnlyDbContext context) : base(context) { }

        public async override Task<PagedList<BooleanProperty>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<BooleanProperty>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.Contains(parameters.SearchQuery))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<BooleanProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
