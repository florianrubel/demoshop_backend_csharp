using LibContent.Models.ContentElement;
using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibContent.Models.Entities
{
    public class ViewContentElementType : ViewUuidBaseEntity
    {
        [SwaggerParameter(LibContent.Entities.ContentElementType.DOC_Name)]
        public string Name { get; set; }

        [SwaggerParameter(LibContent.Entities.ContentElementType.DOC_Slug)]
        public string Slug { get; set; }

        [SwaggerParameter(LibContent.Entities.ContentElementType.DOC_FieldConfig)]
        public Dictionary<string, FieldConfig> FieldConfigs { get; set; }
    }
}
