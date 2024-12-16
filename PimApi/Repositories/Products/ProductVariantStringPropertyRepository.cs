using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;

namespace PimApi.Repositories.Products
{
    public class ProductVariantStringPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariantStringProperty, ProductVariantStringPropertySearchParameters>
        , IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters>
    {
        public ProductVariantStringPropertyRepository(MainDbContext context) : base(context) { }

        public async override Task<PagedList<ProductVariantStringProperty>> GetMultiple(ProductVariantStringPropertySearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<ProductVariantStringProperty>;

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

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Value != null && r.Value.Contains(parameters.SearchQuery))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<ProductVariantStringProperty>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}
