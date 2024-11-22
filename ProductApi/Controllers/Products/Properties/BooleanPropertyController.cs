using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Entities.Products.Properties;
using ProductApi.Models.Products.Properties.BooleanProperty;
using ProductApi.Repositories.Products.Properties;
using Shared.Controllers;
using Shared.Models.Api;

namespace ProductApi.Controllers.Products.Properties
{
    [Route("properties/[controller]")]
    public class BooleanPropertyController : DefaultControllerTemplate<BooleanProperty, ViewBooleanProperty, CreateBooleanProperty, PatchBooleanProperty, SearchParameters>
    {
        public BooleanPropertyController(IMapper mapper, IBooleanPropertyRepository<BooleanProperty, SearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
