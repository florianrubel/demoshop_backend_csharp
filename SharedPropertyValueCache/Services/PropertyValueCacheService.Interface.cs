
namespace SharedPropertyValueCache.Services
{
    public interface IPropertyValueCacheService : IPropertyValueCacheReadOnlyService
    {
        Task SetValuesForProperty(Guid propertyId, IEnumerable<string> values);
    }
}