using LibContent.Entities;

namespace ContentApi.Services.ProductCache
{
    public interface IContentCacheService
    {
        Task<Page?> GetContentCache(Guid id);
        Task<Page> SetContentCache(Page page);
    }
}