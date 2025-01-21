using Shared.Models;

namespace SharedProducts.Models.Products.Properties
{
    public abstract class AbstractViewProperty : UuidViewModel
    {
        public string Name { get; set; }
    }
}
