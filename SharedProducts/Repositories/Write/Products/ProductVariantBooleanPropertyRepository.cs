using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Services.ProductCache;

namespace SharedProducts.Repositories.Write.Products
{
    public class ProductVariantBooleanPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
        , IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
    {
        private readonly IProductCacheService _productCacheService;

        public ProductVariantBooleanPropertyRepository(MainDbContext context, IProductCacheService productCacheService) : base(context)
        {
            _productCacheService = productCacheService;
        }

        public async override Task<ProductVariantBooleanProperty> Create(ProductVariantBooleanProperty entity)
        {
            var result = await base.Create(entity);

            _productCacheService.BuildByBooleanProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<ProductVariantBooleanProperty>> CreateRange(IEnumerable<ProductVariantBooleanProperty> entities)
        {
            var result = await base.CreateRange(entities);

            _productCacheService.BuildByBooleanProperties(from entity in result select entity.Id);

            return result;
        }

        public async override Task Delete(ProductVariantBooleanProperty entity)
        {
            _productCacheService.BuildByProductVariants(new List<Guid> { entity.ProductVariantId });
            await base.Delete(entity);
        }

        public override Task DeleteRange(IEnumerable<ProductVariantBooleanProperty> entities)
        {
            _productCacheService.BuildByProductVariants((from entity in entities select entity.ProductVariantId).Distinct());
            return base.DeleteRange(entities);
        }

        public async override Task<PagedList<ProductVariantBooleanProperty>> GetMultiple(ProductVariantBooleanPropertyPaginationParameters parameters)
        {
            var collection = _dbSet as IQueryable<ProductVariantBooleanProperty>;

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

            var pagedList = await PagedList<ProductVariantBooleanProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public async override Task<ProductVariantBooleanProperty> Update(ProductVariantBooleanProperty entity, ProductVariantBooleanProperty? oldEntity = null)
        {
            var result = await base.Update(entity, oldEntity);

            _productCacheService.BuildByBooleanProperties(new List<Guid> { result.Id });

            return result;
        }

        public async override Task<IEnumerable<ProductVariantBooleanProperty>> UpdateRange(IEnumerable<ProductVariantBooleanProperty> entities, IDictionary<Guid, ProductVariantBooleanProperty>? oldEntities = null)
        {
            var result = await base.UpdateRange(entities, oldEntities);

            _productCacheService.BuildByBooleanProperties(from entity in result select entity.Id);

            return result;
        }
    }
}
