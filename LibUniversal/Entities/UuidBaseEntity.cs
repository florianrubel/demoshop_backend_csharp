using MongoDB.Bson.Serialization.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace LibUniversal.Entities
{
    public abstract class UuidBaseEntity : BaseEntity
    {
        public const string DOC_Id = "The unique primary key of the entity.";

        [Key]
        [BsonId]
        [SwaggerParameter(DOC_Id)]
        public Guid Id { get; set; }
    }
}
