namespace SharedProducts.Models.Products.Properties.StringProperty
{
    public class PatchStringProperty : AbstractPatchProperty
    {
        public List<string> AllowedValues { get; set; } = new List<string>();
    }
}
