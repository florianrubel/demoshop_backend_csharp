using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantNumericProperty;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;

namespace ProductCacheApi.Repositories.Products
{
    public class ProductVariantNumericPropertyRepository
        : Shared.Repositories.UuidReadOnlyRepository<ReadOnlyDbContext, ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
        , IProductVariantNumericPropertyRepository<ProductVariantNumericProperty, ProductVariantNumericPropertyPaginationParameters>
    {
        public ProductVariantNumericPropertyRepository(ReadOnlyDbContext context) : base(context) { }

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
    }
}
