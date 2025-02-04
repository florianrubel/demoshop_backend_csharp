using Shared.Constants;
using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;
using SharedProducts.DbContexts;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.ProductVariantStringProperty;
using SharedPropertyValueCache.Services;

namespace SharedProducts.Repositories.Write.Products
{
    public class ProductVariantStringPropertyRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, ProductVariantStringProperty, ProductVariantStringPropertySearchParameters>
        , IProductVariantStringPropertyRepository<ProductVariantStringProperty, ProductVariantStringPropertySearchParameters>
    {
        private readonly IPropertyValueCacheService _propertyValueCacheService;

        public ProductVariantStringPropertyRepository(
            MainDbContext context,
            IPropertyValueCacheService propertyValueCacheService
        ) : base(context)
        {
            this._propertyValueCacheService = propertyValueCacheService;
        }

        public async override Task<ProductVariantStringProperty> Create(ProductVariantStringProperty entity)
        {
            var result = await base.Create(entity);

            var values = await _propertyValueCacheService.GetValuesForProperty(entity.PropertyId);

            if (!values.Contains(entity.Value))
            {
                values.Add(entity.Value);
            }

            await _propertyValueCacheService.SetValuesForProperty(entity.PropertyId, values);

            return result;
        }

        public async override Task<IEnumerable<ProductVariantStringProperty>> CreateRange(IEnumerable<ProductVariantStringProperty> entities)
        {
            var result = await base.CreateRange(entities);

            foreach (var entity in entities)
            {
                var values = await _propertyValueCacheService.GetValuesForProperty(entity.PropertyId);

                if (!values.Contains(entity.Value))
                {
                    values.Add(entity.Value);
                }

                await _propertyValueCacheService.SetValuesForProperty(entity.PropertyId, values);
            }

            return result;
        }

        public async override Task Delete(ProductVariantStringProperty entity)
        {
            await base.Delete(entity);

            var existing = await GetMultiple(new ProductVariantStringPropertySearchParameters
            {
                PageSize = 1,
                Value = entity.Value,
            });

            if (existing.Count == 0)
            {
                var values = await _propertyValueCacheService.GetValuesForProperty(entity.PropertyId);
                var cleanedValues = from existingValue in existing where existingValue.Value != entity.Value select entity.Value;
                if (values.Count != cleanedValues.Count())
                {
                    await _propertyValueCacheService.SetValuesForProperty(entity.PropertyId, cleanedValues);
                }
            }
        }

        public async override Task DeleteRange(IEnumerable<ProductVariantStringProperty> entities)
        {
            await base.DeleteRange(entities);

            foreach (var entity in entities)
            {
                var existing = await GetMultiple(new ProductVariantStringPropertySearchParameters
                {
                    PageSize = 1,
                    Value = entity.Value,
                });

                if (existing.Count == 0)
                {
                    var values = await _propertyValueCacheService.GetValuesForProperty(entity.PropertyId);
                    var cleanedValues = from existingValue in existing where existingValue.Value != entity.Value select entity.Value;
                    if (values.Count != cleanedValues.Count())
                    {
                        await _propertyValueCacheService.SetValuesForProperty(entity.PropertyId, cleanedValues);
                    }
                }
            }
        }

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

            if (parameters.Values != null)
            {
                collection = collection.Where(r =>
                    parameters.Values.Contains(r.Value)
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

        public async override Task<ProductVariantStringProperty> Update(ProductVariantStringProperty entity, ProductVariantStringProperty oldEntity)
        {
            var result = await base.Update(entity);

            var oldExisting = await GetMultiple(new ProductVariantStringPropertySearchParameters
            {
                PageSize = 1,
                Value = oldEntity.Value,
            });

            var values = await _propertyValueCacheService.GetValuesForProperty(entity.PropertyId);

            if (oldExisting.Count() == 0)
            {
                values = (from value in values where value != oldEntity.Value select value).ToList();
            }

            if (!values.Contains(entity.Value))
            {
                values.Add(entity.Value);
            }

            await _propertyValueCacheService.SetValuesForProperty(entity.PropertyId, values);

            return result;
        }

        public async Task<IEnumerable<ProductVariantStringProperty>> UpdateRange(IEnumerable<ProductVariantStringProperty> entities, Dictionary<Guid, ProductVariantStringProperty> oldEntities)
        {
            var result = await base.UpdateRange(entities);

            foreach (var entity in entities)
            {
                var oldEntity = oldEntities.GetValueOrDefault(entity.Id);
                if (oldEntity == null) { continue; }
                var oldValue = oldEntity.Value;
                var oldExisting = await GetMultiple(new ProductVariantStringPropertySearchParameters
                {
                    PageSize = 1,
                    Value = oldValue,
                });

                var values = await _propertyValueCacheService.GetValuesForProperty(entity.PropertyId);

                if (oldExisting.Count() == 0)
                {
                    values = (from value in values where value != oldValue select value).ToList();
                }

                if (!values.Contains(entity.Value))
                {
                    values.Add(entity.Value);
                }

                await _propertyValueCacheService.SetValuesForProperty(entity.PropertyId, values);
            }

            return result;
        }
    }
}
