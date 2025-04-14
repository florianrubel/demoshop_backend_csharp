using LibUniversal.Models.Entities.BaseEntity;
using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Models.Entities.RecursiveEntity
{
    public class CreateRecursiveEntity<EntityType> : CreateBaseEntity
    {
        [SwaggerParameter(LibUniversal.Entities.RecursiveEntity<EntityType>.DOC_ParentId)]
        public Guid? ParentId { get; set; }

        [SwaggerParameter(LibUniversal.Entities.RecursiveEntity<EntityType>.DOC_Order)]
        public int Order { get; set; }
    }
}
