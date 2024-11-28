using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.Product
{
    public class CreateProduct
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        [MaxLength(InputSizes.MULTILINE_TEXT_MAX_LENGTH)]
        public string Description { get; set; }

        public string ListPicture { get; set; }

        public List<string> Pictures { get; set; } = new List<string>();

        public int DefaultPriceInCents { get; set; }
    }
}
