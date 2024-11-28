using System.ComponentModel.DataAnnotations;

namespace Shared.Entities
{
    public abstract class UuidBaseEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
