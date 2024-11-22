using ProductApi.Entities.Products.Properties;
using ProductApi.Models.Products.Properties.StringProperty;
using Shared.Profiles;

namespace ProductApi.Profiles.Products.Properties
{
    public class StringPropertyProfile : DefaultProfile<StringProperty, ViewStringProperty, CreateStringProperty, PatchStringProperty>
    {
    }
}
