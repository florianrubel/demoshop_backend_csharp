using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.Properties.StringProperty
{
    public class PatchStringProperty
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string? Name { get; set; }

        public List<string> AllowedValues { get; set; } = new List<string>();
    }
}
