
namespace SharedStockCache.Services
{
    public interface IStockItemCacheService : IStockItemCacheReadOnlyService
    {
        Task SetStockAmountForProductVariant(Guid id, int amount);
    }
}