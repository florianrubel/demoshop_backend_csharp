using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Entities.Products
{
    public class Product : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Description { get; set; }

        public int DefaultPriceInCents { get; set; }
    }
}
