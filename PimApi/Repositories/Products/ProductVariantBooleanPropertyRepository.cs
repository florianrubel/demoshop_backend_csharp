using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantBooleanProperty;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;

namespace PimApi.Repositories.Products
{
    public class ProductVariantBooleanPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
        , IProductVariantBooleanPropertyRepository<ProductVariantBooleanProperty, ProductVariantBooleanPropertyPaginationParameters>
    {
        public ProductVariantBooleanPropertyRepository(MainDbContext context) : base(context) { }

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
    }
}
