using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;
using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;

namespace PimApi.Repositories.Products
{
    public class ProductVariantRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariant, ProductVariantPaginationParameters>
        , IProductVariantRepository<ProductVariant, ProductVariantPaginationParameters>
    {
        public ProductVariantRepository(MainDbContext context) : base(context) { }

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
    }
}
