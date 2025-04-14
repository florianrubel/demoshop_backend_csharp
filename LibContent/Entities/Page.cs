using LibUniversal.Constants;
using LibUniversal.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibContent.Entities
{
    public class Page : LocalizedEntity<Page>
    {
        public const string DOC_Title = "Title of the page. This is used for the page meta.";
        public const string DOC_InternalTitle = "Can be used for internal visualization. If you have a long page title, you can have a shorter internal name for better readability.";
        public const string DOC_NavigationTitle = "This is used in menus and navigations.";
        public const string DOC_MetaDescription = "A short description of the page content for the page meta.";
        public const string DOC_MetaTags = "Additional meta tags" +
                                            "```json" +
                                            "{" +
                                            "    \"og:image\": \"<some-url-to-an-image\"" +
                                            "}" +
                                            "```";
        public const string DOC_IsRoot = "This page is a root page for a page tree";
        public const string DOC_Domain = "This page and it's subpages should be accessible under this domain.";
        public const string DOC_Slug = "The slugified page title for routing";
        public const string DOC_LockSlug = "If true, the slug won't get automatically generated. If you want to give a custom slug for example.";

        [SwaggerParameter(DOC_Title)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Title { get; set; }

        [SwaggerParameter(DOC_InternalTitle)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? InternalName { get; set; }

        [SwaggerParameter(DOC_NavigationTitle)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? NavigationTitle { get; set; }

        [SwaggerParameter(DOC_MetaDescription)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? MetaDescription { get; set; }

        [SwaggerParameter(DOC_MetaTags)]
        public Dictionary<string, string>? MetaTags { get; set; }

        [SwaggerParameter(DOC_IsRoot)]
        public bool IsRoot { get; set; } = false;

        [SwaggerParameter(DOC_Domain)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Domain { get; set; }

        [SwaggerParameter(DOC_Slug)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string slug { get; set; }

        [SwaggerParameter(DOC_LockSlug)]
        public bool LockSlug { get; set; }
    }
}
