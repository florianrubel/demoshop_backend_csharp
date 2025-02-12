using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;
using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products
{
    public class ProductVariantRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariant, ProductVariantPaginationParameters>
        , IProductVariantRepository<ProductVariant, ProductVariantPaginationParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public ProductVariantRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<ProductVariant> Create(ProductVariant entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByProductVariants(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<ProductVariant>> CreateRange(IEnumerable<ProductVariant> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByProductVariants(from entity in result select entity.Id);

            return result;
        }

        public async override Task<PagedList<ProductVariant>> GetMultiple(ProductVariantPaginationParameters parameters)
        {
            var collection = _dbSet as IQueryable<ProductVariant>;

            if (parameters.ProductIds != null)
            {
                var propuctIds = TextService.GetGuidArray(parameters.ProductIds);
                collection = collection.Where(r => propuctIds.Contains(r.ProductId));
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<ProductVariant>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<ProductVariant> Update(ProductVariant entity, ProductVariant? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByProductVariants(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<ProductVariant>> UpdateRange(IEnumerable<ProductVariant> entities, IDictionary<Guid, ProductVariant>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByProductVariants(from entity in result select entity.Id);

            return result;
        }
    }
}
