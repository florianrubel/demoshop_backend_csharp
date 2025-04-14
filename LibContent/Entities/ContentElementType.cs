using LibContent.Models.ContentElement;
using LibUniversal.Constants;
using LibUniversal.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibContent.Entities
{
    public class ContentElementType : UuidBaseEntity
    {
        public const string DOC_Name = "The name of the content element type.";
        public const string DOC_Slug = "The slugified name used to rendere it in the frontend.";
        public const string DOC_FieldConfig = "Describes which properties with which data types are supported by this content type.";

        [SwaggerParameter(DOC_Name)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [SwaggerParameter(DOC_Slug)]
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Slug { get; set; }

        [SwaggerParameter(DOC_FieldConfig)]
        public Dictionary<string, FieldConfig> FieldConfigs { get; set; }
    }
}
