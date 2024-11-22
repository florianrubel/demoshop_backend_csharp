using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Entities.Products.Properties
{
    public class StringProperty : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        public List<string> AllowedValues { get; set; } = new List<string>();
    }
}
