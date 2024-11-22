using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entities.Products.Properties;
using ProductApi.Models.Products.Properties.NumericProperty;
using ProductApi.Repositories.Products.Properties;
using Shared.Controllers;
using Shared.Models.Api;

namespace ProductApi.Controllers.Products.Properties
{
    [Route("properties/[controller]")]
    public class NumericPropertyController : DefaultControllerTemplate<NumericProperty, ViewNumericProperty, CreateNumericProperty, PatchNumericProperty, SearchParameters>
    {
        public NumericPropertyController(IMapper mapper, INumericPropertyRepository<NumericProperty, SearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
