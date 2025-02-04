namespace SharedProducts.Models.Products.ProductVariantStringProperty
{
    public class ProductVariantStringPropertySearchParameters : AbstractProductVariantSearchParameters<string?>
    {
        public IEnumerable<string>? Values { get; set; }
    }
}
