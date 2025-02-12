using ProductCacheApi.Models.Hubs.ProductCache;

namespace ProductCacheApi.Hubs
{
    public interface IProductCacheHub
    {
        Task CacheProgress(CacheProgress progress);
    }
}
