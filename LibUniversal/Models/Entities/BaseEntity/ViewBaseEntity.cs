using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.BaseEntity
{
    public class ViewBaseEntity
    {
        [SwaggerParameter(LibUniversal.Entities.BaseEntity.DOC_Deleted)]
        public bool Deleted { get; set; }

        [SwaggerParameter(LibUniversal.Entities.BaseEntity.DOC_CreatedAt)]
        public DateTimeOffset CreatedAt { get; set; }

        [SwaggerParameter(LibUniversal.Entities.BaseEntity.Doc_UpdatedAt)]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
