using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibUniversal.Entities
{
    public class RecursiveEntity<ParentType> : UuidBaseEntity
    {
        public const string DOC_ParentId = "The guid of the parent entity.";
        public const string DOC_Parent = "The lazy loaded parent entity object.";
        public const string DOC_Order = "The order position of this entity in it's level.";
        public const string DOC_Children = "A lazy loaded list of child entities.";

        [SwaggerParameter(DOC_ParentId)]
        [ForeignKey(nameof(ParentId))]
        public Guid? ParentId { get; set; }
        [SwaggerParameter(DOC_Parent)]
        public virtual ParentType? Parent { get; set; }


        [SwaggerParameter(DOC_Order)]
        public int Order { get; set; }

        [SwaggerParameter(DOC_Children)]
        public virtual List<ParentType> Children { get; set; } = new List<ParentType>();
    }
}
