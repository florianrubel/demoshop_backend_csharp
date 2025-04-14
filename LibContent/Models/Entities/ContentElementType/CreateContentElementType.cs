using LibContent.Models.ContentElement;
using LibUniversal.Constants;
using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibContent.Models.Entities.ContentElementType
{
    public class CreateContentElementType : CreateUuidBaseEntity
    {
        [SwaggerParameter(LibContent.Entities.ContentElementType.DOC_Name)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [SwaggerParameter(LibContent.Entities.ContentElementType.DOC_Slug)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Slug { get; set; }

        [SwaggerParameter(LibContent.Entities.ContentElementType.DOC_FieldConfig)]
        public Dictionary<string, FieldConfig> FieldConfigs { get; set; }
    }
}
