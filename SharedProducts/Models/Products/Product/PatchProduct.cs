using Shared.Constants;
using SharedProducts.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.Product
{
    public class PatchProduct
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }
        [LocalizedField]
        public Dictionary<string, string> DescriptionLocalized { get; set; }

        public int? DefaultPriceInCents { get; set; }
    }
}
