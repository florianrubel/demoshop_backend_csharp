using LibContent.Entities;

namespace ContentCacheApi.Services
{
    public interface IContentCacheService
    {
        Task<Page?> GetPageCache(Guid id);
        Task<Page> SetPageCache(Page page);
    }
}