using LibUniversal.Constants;
using LibUniversal.Models.Entities.LocalizedEntity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibContent.Models.Entities.Page
{
    public class CreatePage : CreateLocalizedEntity<CreatePage>
    {
        [SwaggerParameter(LibContent.Entities.Page.DOC_Title)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Title { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_InternalTitle)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? InternalName { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_NavigationTitle)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? NavigationTitle { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_MetaDescription)]
        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string? MetaDescription { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_MetaTags)]
        public Dictionary<string, string>? MetaTags { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_IsRoot)]
        public bool IsRoot { get; set; } = false;

        [SwaggerParameter(LibContent.Entities.Page.DOC_Domain)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Domain { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_Slug)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string slug { get; set; }

        [SwaggerParameter(LibContent.Entities.Page.DOC_LockSlug)]
        public bool LockSlug { get; set; }
    }
}
