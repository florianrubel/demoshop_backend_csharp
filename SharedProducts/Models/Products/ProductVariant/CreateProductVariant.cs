using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Models.Products.ProductVariant
{
    public class CreateProductVariant
    {
        public int PriceInCents { get; set; }

        [Required]
        public Guid? ProductId { get; set; }

        public string? ListPicture { get; set; }

        public List<string> Pictures { get; set; } = new List<string>();
    }
}
