using ProductApi.DbContexts;
using ProductApi.Entities.Products;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;

namespace ProductApi.Repositories.Products
{
    public class ProductRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, Product, SearchParameters>
        , IProductRepository<Product, SearchParameters>
    {
        public ProductRepository(MainDbContext context) : base(context) { }

        public async override Task<PagedList<Product>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<Product>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.Contains(parameters.SearchQuery))
                );
                collection = collection.Where(r =>
                    (r.Description != null && r.Description.Contains(parameters.SearchQuery))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<Product>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
