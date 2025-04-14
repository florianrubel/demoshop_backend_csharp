using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.BaseEntity
{
    public class CreateBaseEntity
    {
        [SwaggerParameter(LibUniversal.Entities.BaseEntity.DOC_Deleted)]
        public bool? Deleted { get; set; }
    }
}
