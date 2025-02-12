
namespace SharedProducts.Services.ProductCache
{
    public interface IProductCacheService
    {
        Task BuildByBooleanProperties(IEnumerable<Guid> ids);
        Task BuildByNumericProperties(IEnumerable<Guid> ids);
        Task BuildByProductVariants(IEnumerable<Guid> ids);
        Task BuildByProductVariantBooleanProperties(IEnumerable<Guid> ids);
        Task BuildByProductVariantNumericProperties(IEnumerable<Guid> ids);
        Task BuildByBooleanPropertys(IEnumerable<Guid> ids);
        Task BuildByProductVariantStringProperties(IEnumerable<Guid> ids);
        Task BuildByStringProperties(IEnumerable<Guid> ids);
        Task BuildCache();
    }
}