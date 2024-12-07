﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedProducts.Entities.Products;
using SharedProducts.Models.Products.Product;
using PimApi.Repositories.Products;
using Shared.Controllers;
using Shared.Models.Api;

namespace PimApi.Controllers.Products
{
    [Route("products")]
    [Authorize(Roles = Shared.Constants.Identity.AUTHORIZE_MIN_ADMIN)]
    public class ProductController : DefaultControllerTemplate<Product, ViewProduct, CreateProduct, PatchProduct, ISearchParameters>
    {
        public ProductController(IMapper mapper, IProductRepository<Product, ISearchParameters> repository) : base(mapper, repository)
        {
        }
    }
}
