
namespace SharedPropertyValueCache.Services
{
    public interface IPropertyValueCacheReadOnlyService
    {
        Task<List<string>> GetValuesForProperty(Guid propertyId);
    }
}