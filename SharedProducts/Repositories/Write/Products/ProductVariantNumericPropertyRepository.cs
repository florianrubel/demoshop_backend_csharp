using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products
{
    public class ProductVariantNumericPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
        , IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public ProductVariantNumericPropertyRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<ProductVariantNumericProperty> Create(ProductVariantNumericProperty entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByNumericProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<ProductVariantNumericProperty>> CreateRange(IEnumerable<ProductVariantNumericProperty> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByNumericProperties(from entity in result select entity.Id);

            return result;
        }

        public async override Task Delete(ProductVariantNumericProperty entity)
        {
            _productCacheService.BuildByProductVariants(new List<Guid> { entity.ProductVariantId });
            await base.Delete(entity);
        }

        public override Task DeleteRange(IEnumerable<ProductVariantNumericProperty> entities)
        {
            _productCacheService.BuildByProductVariants((from entity in entities select entity.ProductVariantId).Distinct());
            return base.DeleteRange(entities);
        }

        public async override Task<PagedList<ProductVariantNumericProperty>> GetMultiple(ProductVariantNumericPropertyPaginationParameters parameters)
        {
            var collection = _dbSet as IQueryable<ProductVariantNumericProperty>;

            if (parameters.ProductVariantIds != null)
            {
                var productVariantIds = TextService.GetGuidArray(parameters.ProductVariantIds);
                collection = collection.Where(r => productVariantIds.Contains(r.ProductVariantId));
            }

            if (parameters.PropertyIds != null)
            {
                var propertyIds = TextService.GetGuidArray(parameters.PropertyIds);
                collection = collection.Where(r => propertyIds.Contains(r.PropertyId));
            }

            if (parameters.Value != null)
            {
                collection = collection.Where(r =>
                    r.Value == parameters.Value
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<ProductVariantNumericProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<ProductVariantNumericProperty> Update(ProductVariantNumericProperty entity, ProductVariantNumericProperty? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByNumericProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<ProductVariantNumericProperty>> UpdateRange(IEnumerable<ProductVariantNumericProperty> entities, IDictionary<Guid, ProductVariantNumericProperty>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByNumericProperties(from entity in result select entity.Id);

            return result;
        }
    }
}
