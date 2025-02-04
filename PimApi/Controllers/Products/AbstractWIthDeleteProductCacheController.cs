using AutoMapper;
using PimApi.Cache;
using Shared.Controllers;
using Shared.Entities;
using Shared.Models.Api;
using Shared.Repositories;

namespace PimApi.Controllers.Products
{
    public abstract class AbstractWithDeleteProductCacheController<EntityType, ViewType, CreateType, PatchType, PaginationParametersType>
        : WithDeleteDefaultControllerTemplate<EntityType, ViewType, CreateType, PatchType, PaginationParametersType>
        where EntityType : UuidBaseEntity
        where PatchType : class
        where PaginationParametersType : PaginationParameters
    {
        protected readonly IProductCacheFactory _productCacheFactory;

        public AbstractWithDeleteProductCacheController(
            IMapper mapper,
            IUuidBaseRepository<EntityType, PaginationParametersType> repository,
            IProductCacheFactory productCacheFactory
        ) : base(mapper, repository)
        {
            _productCacheFactory = productCacheFactory;
        }
    }
}
