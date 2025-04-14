using LibUniversal.Models.Entities.UuidBaseEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.RecursiveEntity
{
    public class ViewRecursiveEntity<EntityType> : ViewUuidBaseEntity
    {
        [SwaggerParameter(LibUniversal.Entities.RecursiveEntity<EntityType>.DOC_ParentId)]
        public Guid? ParentId { get; set; }

        [SwaggerParameter(LibUniversal.Entities.RecursiveEntity<EntityType>.DOC_Order)]
        public int Order { get; set; }
    }
}
