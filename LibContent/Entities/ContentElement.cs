using LibUniversal.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibContent.Entities
{
    public class ContentElement : LocalizedEntity<ContentElement>
    {
        public const string DOC_PageId = "The page id where the content is stored. Should always be the localized version of the page.";
        public const string DOC_Page = "The lazy loaded page object where the content is stored.";
        public const string DOC_ContentElementTypeId = "The id of the content element type.";
        public const string DOC_ContentElementType = "The lazy loaded object of the content element type.";
        public const string DOC_Properties = "Each content element type can have different properties. These are configured here.";

        [ForeignKey(nameof(PageId))]
        public Guid? PageId { get; set; }
        public Page? Page { get; set; }

        [ForeignKey(nameof(ContentElementTypeId))]
        public Guid ContentElementTypeId { get; set; }
        public ContentElementType ContentElementType { get; set; }

        public Dictionary<string, string> Properties { get; set; }
    }
}
