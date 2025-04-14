using LibContent.Entities;
using LibUniversal.Entities;
using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibContent.Models.Cache
{
    public class PageCacheItem : ViewUuidBaseEntity
    {
        [SwaggerParameter(Page.DOC_Title)]
        public string Title { get; set; }

        [SwaggerParameter(Page.DOC_NavigationTitle)]
        public string? NavigationTitle { get; set; }

        [SwaggerParameter(Page.DOC_MetaDescription)]
        public string? MetaDescription { get; set; }

        [SwaggerParameter(Page.DOC_MetaTags)]
        public Dictionary<string, string>? MetaTags { get; set; }

        [SwaggerParameter(Page.DOC_IsRoot)]
        public bool IsRoot { get; set; } = false;

        [SwaggerParameter(Page.DOC_Domain)]
        public string? Domain { get; set; }

        [SwaggerParameter(RecursiveEntity<PageCacheItem>.DOC_Order)]
        public int Order { get; set; }

        [SwaggerParameter(Page.DOC_Slug)]
        public string slug { get; set; }

        [SwaggerParameter(LocalizedEntity<PageCacheItem>.DOC_LocaleId)]
        public Guid LocaleId { get; set; }

        public List<ContentElementCacheItem> ContentElements { get; set; }
    }
}
