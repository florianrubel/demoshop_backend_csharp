using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.BooleanProperty;
using PimApi.Repositories.Products.Properties;
using Shared.Controllers;
using Shared.Models.Api;

namespace PimApi.Controllers.Products.Properties
{
    [Route("properties/boolean-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class BooleanPropertyController : DefaultControllerTemplate<BooleanProperty, ViewBooleanProperty, CreateBooleanProperty, PatchBooleanProperty, ISearchParameters>
    {
        public BooleanPropertyController(IMapper mapper, IBooleanPropertyRepository<BooleanProperty, ISearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
