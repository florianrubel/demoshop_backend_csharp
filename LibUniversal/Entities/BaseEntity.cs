using Swashbuckle.AspNetCore.Annotations;

namespace LibUniversal.Entities
{
    public abstract class BaseEntity
    {
        public const string DOC_Deleted = "Entities should normally be flagged as deleted instead of actually deleting them. This flag prevents the entity to be shown publically.";
        public const string DOC_CreatedAt = "The creation date time of the entity with timezone.";
        public const string Doc_UpdatedAt = "The last update of the entity as a date time with timezone.";

        [SwaggerParameter(DOC_Deleted)]
        public bool Deleted { get; set; }

        [SwaggerParameter(DOC_CreatedAt)]
        public DateTimeOffset CreatedAt { get; set; }

        [SwaggerParameter(Doc_UpdatedAt)]
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
