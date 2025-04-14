using LibContent.Entities;
using LibUniversal.Entities;
using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibContent.Models.Cache
{
    public class ContentElementCacheItem : ViewUuidBaseEntity
    {
        [SwaggerParameter(ContentElementType.DOC_Slug)]
        public string ContentElementTypeSlug { get; set; }

        [SwaggerParameter(Entities.ContentElement.DOC_Properties)]
        public Dictionary<string, string> Properties { get; set; }

        [SwaggerParameter(RecursiveEntity<PageCacheItem>.DOC_Order)]
        public int Order { get; set; }

        [SwaggerParameter(LocalizedEntity<ContentElementCacheItem>.DOC_LocaleId)]
        public Guid LocaleId { get; set; }
    }
}
