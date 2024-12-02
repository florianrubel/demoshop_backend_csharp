using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;

namespace PimApi.Repositories.Products
{
    public class ProductVariantRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariant, IProductVariantPaginationParameters>
        , IProductVariantRepository<ProductVariant, IProductVariantPaginationParameters>
    {
        public ProductVariantRepository(MainDbContext context) : base(context) { }

        public async override Task<PagedList<ProductVariant>> GetMultiple(IProductVariantPaginationParameters parameters)
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
