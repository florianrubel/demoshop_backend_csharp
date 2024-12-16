using Shared.Helpers;
using Shared.Models.Api;
using Shared.StaticServices;
using StockApi.DbContexts;
using StockApi.Entities.Stock;
using StockApi.Models.Stock.StockItem;

namespace StockApi.Repositories.Stock
{
    public class StockItemRepository
        : Shared.Repositories.UuidBaseRepository<MainDbContext, StockItem, StockItemPaginationParameters>
        , IStockItemRepository<StockItem, StockItemPaginationParameters>
    {
        private readonly SharedStockCache.Services.IStockItemCacheService _stockItemCacheService;

        public StockItemRepository(MainDbContext context, SharedStockCache.Services.IStockItemCacheService stockItemCacheService) : base(context)
        {
            _stockItemCacheService = stockItemCacheService;
        }

        public override async Task<StockItem> Create(StockItem entity)
        {
            await RecalculateProductVariantStockCache(entity.ProductVariantId);
            return await base.Create(entity);
        }

        public override async Task<IEnumerable<StockItem>> CreateRange(IEnumerable<StockItem> entities)
        {
            foreach (var entity in entities)
            {
                await RecalculateProductVariantStockCache(entity.ProductVariantId);
            }
            return await base.CreateRange(entities);
        }

        public override async Task Delete(StockItem entity)
        {
            await RecalculateProductVariantStockCache(entity.ProductVariantId);
            await base.Delete(entity);
        }

        public override async Task DeleteRange(IEnumerable<StockItem> entities)
        {
            foreach (var entity in entities)
            {
                await RecalculateProductVariantStockCache(entity.ProductVariantId);
            }
            await base.DeleteRange(entities);
        }

        public async override Task<PagedList<StockItem>> GetMultiple(StockItemPaginationParameters parameters)
        {
            var collection = _dbSet as IQueryable<StockItem>;

            if (parameters.ProductVariantIds != null)
            {
                var productVariantIds = TextService.GetGuidArray(parameters.ProductVariantIds);
                collection = collection.Where(r => productVariantIds.Contains(r.ProductVariantId));
            }

            if (parameters.IsAvailable == true)
            {
                var reservationEnd = DateTimeOffset.Now;
                reservationEnd.AddMinutes(SharedStockCache.Constants.Stock.RESERVATION_DURATION * -1);
                collection = collection.Where(r => r.SoldAt == null && r.ReservedAt > reservationEnd);
            }

            collection = collection.ApplySort(parameters.OrderBy);

            var pagedList = await PagedList<StockItem>.Create(collection, parameters.Page, parameters.PageSize);

            return pagedList;
        }

        public override async Task<StockItem> Update(StockItem entity)
        {
            await RecalculateProductVariantStockCache(entity.ProductVariantId);
            return await base.Update(entity);
        }

        public override async Task<IEnumerable<StockItem>> UpdateRange(IEnumerable<StockItem> entities)
        {
            foreach (var entity in entities)
            {
                await RecalculateProductVariantStockCache(entity.ProductVariantId);
            }
            return await base.UpdateRange(entities);
        }

        private async Task RecalculateProductVariantStockCache(Guid productVariantId)
        {
            var parameters = new StockItemPaginationParameters
            {
                ProductVariantIds = productVariantId.ToString(),
                IsAvailable = true,
                PageSize = 1
            };

            var amount = (await GetMultiple(parameters)).TotalCount;

            await _stockItemCacheService.SetStockAmountForProductVariant(productVariantId, amount);
        }
    }
}
