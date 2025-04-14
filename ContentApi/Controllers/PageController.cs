using AutoMapper;
using ContentApi.Repositories;
using LibContent.Entities;
using LibContent.Models.Entities.Page;
using LibDb.Controllers;
using LibUniversal.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace ContentApi.Controllers
{
    [Route("pages")]
    public class ProductController : DefaultControllerTemplate<Page, ViewPage, CreatePage, PatchPage, LocalizedSearchParameters>
    {

        public ProductController(
            IMapper mapper,
            IPageRepository<Page, LocalizedSearchParameters> repository
        ) : base(mapper, repository)
        { }
    }
}
