using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Entities.Products.Properties
{
    public class BooleanProperty : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }
    }
}
