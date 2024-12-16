using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.Properties.NumericProperty
{
    public class PatchNumericProperty
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }
    }
}
