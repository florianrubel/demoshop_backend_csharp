﻿using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariant;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;

namespace ProductCacheApi.Repositories.Products
{
    public class ProductVariantRepository
        : Shared.Repositories.UuidReadOnlyRepository<ReadOnlyDbContext, ProductVariant, IProductVariantSearchParameters>
        , IProductVariantRepository<ProductVariant, IProductVariantSearchParameters>
    {
        public ProductVariantRepository(ReadOnlyDbContext context) : base(context) { }

        public async override Task<PagedList<ProductVariant>> GetMultiple(IProductVariantSearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<ProductVariant>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.Contains(parameters.SearchQuery))
                );
            }

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
