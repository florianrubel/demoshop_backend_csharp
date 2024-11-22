using ProductApi.Entities.Products.Properties;
using ProductApi.Models.Products.Properties.NumericProperty;
using Shared.Profiles;

namespace ProductApi.Profiles.Products.Properties
{
    public class NumericPropertyProfile : DefaultProfile<NumericProperty, ViewNumericProperty, CreateNumericProperty, PatchNumericProperty>
    {
    }
}
