using ProductApi.Entities.Products.Properties;
using ProductApi.Models.Products.Properties.BooleanProperty;
using Shared.Profiles;

namespace ProductApi.Profiles.Products.Properties
{
    public class BooleanPropertyProfile : DefaultProfile<BooleanProperty, ViewBooleanProperty, CreateBooleanProperty, PatchBooleanProperty>
    {
    }
}
