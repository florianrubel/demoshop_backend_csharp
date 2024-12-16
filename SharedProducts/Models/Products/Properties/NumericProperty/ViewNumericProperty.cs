using Shared.Models;

namespace SharedProducts.Models.Products.Properties.NumericProperty
{
    public class ViewNumericProperty : UuidViewModel
    {
        public string Name { get; set; }

        public double? MinValue { get; set; }

        public double? MaxValue { get; set; }
    }
}
