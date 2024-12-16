using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.StringProperty;
using Shared.Profiles;

namespace SharedProducts.Profiles.Products.Properties
{
    public class StringPropertyProfile : DefaultProfile<StringProperty, ViewStringProperty, CreateStringProperty, PatchStringProperty>
    {
    }
}
