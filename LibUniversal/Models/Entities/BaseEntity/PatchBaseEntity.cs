using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.BaseEntity
{
    public class PatchBaseEntity
    {
        [SwaggerParameter(LibUniversal.Entities.BaseEntity.DOC_Deleted)]
        public bool? Deleted { get; set; }
    }
}
