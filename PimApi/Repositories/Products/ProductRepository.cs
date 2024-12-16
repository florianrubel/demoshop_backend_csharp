﻿using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;

namespace PimApi.Repositories.Products
{
    public class ProductRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, Product, SearchParameters>
        , IProductRepository<Product, SearchParameters>
    {
        public ProductRepository(MainDbContext context) : base(context) {}

        public async override Task<PagedList<Product>> GetMultiple(SearchParameters parameters)
        {
            var collection = _dbSet as IQueryable<Product>;

            if (parameters.SearchQuery != null && parameters.SearchQuery.Length >= InputSizes.DEFAULT_TEXT_MIN_LENGTH)
            {
                collection = collection.Where(r =>
                    (r.Name != null && r.Name.Contains(parameters.SearchQuery))
                );
                collection = collection.Where(r =>
                    (r.DescriptionLocalized != null && r.DescriptionLocalized.Values.Contains(parameters.SearchQuery))
                );
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<Product>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }
    }
}