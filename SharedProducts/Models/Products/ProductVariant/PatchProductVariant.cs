namespace SharedProducts.Models.Products.ProductVariant
{
    public class PatchProductVariant
    {
        public int? PriceInCents { get; set; }

        public string? ListPicture { get; set; }

        public List<string>? Pictures { get; set; }
    }
}
