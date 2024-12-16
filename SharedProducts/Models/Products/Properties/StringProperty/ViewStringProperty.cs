using Shared.Models;

namespace SharedProducts.Models.Products.Properties.StringProperty
{
    public class ViewStringProperty : UuidViewModel
    {
        public string Name { get; set; }

        public List<string> AllowedValues { get; set; } = new List<string>();
    }
}
