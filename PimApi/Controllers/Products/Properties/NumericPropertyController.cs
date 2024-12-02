using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products.Properties;
using SharedProducts.Models.Products.Properties.NumericProperty;
using PimApi.Repositories.Products.Properties;
using Shared.Controllers;
using Shared.Models.Api;

namespace PimApi.Controllers.Products.Properties
{
    [Route("properties/numeric-properties")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class NumericPropertyController : DefaultControllerTemplate<NumericProperty, ViewNumericProperty, CreateNumericProperty, PatchNumericProperty, ISearchParameters>
    {
        public NumericPropertyController(IMapper mapper, INumericPropertyRepository<NumericProperty, ISearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
