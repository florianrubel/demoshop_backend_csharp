using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products
{
    public class ProductRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, Product, SearchParameters>
        , IProductRepository<Product, SearchParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public ProductRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<Product> Create(Product entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByProductVariants(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<Product>> CreateRange(IEnumerable<Product> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByProductVariants(from entity in result select entity.Id);

            return result;
        }

        public async override Task<PagedList<Product>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<Product>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.ToLower().Contains(parameters.SearchQuery.ToLower()))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<Product>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<Product> Update(Product entity, Product? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByProductVariants(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<Product>> UpdateRange(IEnumerable<Product> entities, IDictionary<Guid, Product>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByProductVariants(from entity in result select entity.Id);

            return result;
        }
    }
}
