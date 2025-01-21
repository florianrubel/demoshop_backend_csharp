namespace SharedProducts.Entities.Products.Properties
{
    public class StringProperty : AbstractProperty
    {
        public List<string> AllowedValues { get; set; } = new List<string>();
    }
}
