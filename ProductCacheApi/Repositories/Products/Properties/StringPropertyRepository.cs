using SharedProducts.DbContexts;
using SharedProducts.Entities.Products.Properties;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;

namespace ProductCacheApi.Repositories.Products.Properties
{
    public class StringPropertyRepository
        : Shared.Repositories.UuidReadOnlyRepository<ReadOnlyDbContext, StringProperty, SearchParameters>
        , IStringPropertyRepository<StringProperty, SearchParameters>
    {
        public StringPropertyRepository(ReadOnlyDbContext context) : base(context) { }

        public async override Task<PagedList<StringProperty>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<StringProperty>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.Contains(parameters.SearchQuery))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<StringProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
