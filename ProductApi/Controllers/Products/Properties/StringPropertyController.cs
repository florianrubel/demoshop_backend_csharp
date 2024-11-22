using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entities.Products.Properties;
using ProductApi.Models.Products.Properties.StringProperty;
using ProductApi.Repositories.Products.Properties;
using Shared.Controllers;
using Shared.Models.Api;

namespace ProductApi.Controllers.Products.Properties
{
    [Route("properties/[controller]")]
    public class StringPropertyController : DefaultControllerTemplate<StringProperty, ViewStringProperty, CreateStringProperty, PatchStringProperty, SearchParameters>
    {
        public StringPropertyController(IMapper mapper, IStringPropertyRepository<StringProperty, SearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
