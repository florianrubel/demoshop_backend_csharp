
namespace SharedStockCache.Services
{
    public interface IStockItemCacheReadOnlyService
    {
        Task<int> GetStockAmountForProductVariant(Guid id);
    }
}