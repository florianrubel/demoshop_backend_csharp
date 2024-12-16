using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PimApi.Repositories.Products;
using Shared.Controllers;
using Shared.Helpers;
using Shared.Models.Api;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.Product;

namespace PimApi.Controllers.Products
{
    [Route("products")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductController : DefaultControllerTemplate<Product, ViewProduct, CreateProduct, PatchProduct, SearchParameters>
    {
        public ProductController(IMapper mapper, IProductRepository<Product, SearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
