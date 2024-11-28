using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.StringProperty;
using PimApi.Repositories.Products.Properties;
using Shared.Controllers;
using Shared.Models.Api;

namespace PimApi.Controllers.Products.Properties
{
    [Route("properties/string-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class StringPropertyController : DefaultControllerTemplate<StringProperty, ViewStringProperty, CreateStringProperty, PatchStringProperty, SearchParameters>
    {
        public StringPropertyController(IMapper mapper, IStringPropertyRepository<StringProperty, SearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
