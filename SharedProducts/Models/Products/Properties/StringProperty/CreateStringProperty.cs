namespace SharedProducts.Models.Products.Properties.StringProperty
{
    public class CreateStringProperty : AbstractCreateProperty
    {
        public List<string> AllowedValues { get; set; } = new List<string>();
    }
}
