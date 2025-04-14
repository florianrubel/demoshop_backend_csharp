using ContentCacheApi.Services;
using LibContent.Entities;
using LibUniversal.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace PageCacheApi.Controllers
{
    [ApiController]
    [Route("page-cache")]
    public class ContentCacheController : BasicControllerTemplate
    {
        private readonly IContentCacheService _pageCacheService;

        public ContentCacheController(IContentCacheService pageCacheService)
        {
            _pageCacheService = pageCacheService;
        }

        [HttpGet]
        [Route("{pageId}")]
        public async Task<ActionResult<Page?>> Get(Guid pageId)
        {
            return await _pageCacheService.GetPageCache(pageId);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Page>> Set([FromBody] Page page)
        {
            return await _pageCacheService.SetPageCache(page);
        }
    }
}
