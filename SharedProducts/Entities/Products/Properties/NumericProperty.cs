using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Entities.Products.Properties
{
    public class NumericProperty : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }
    }
}
