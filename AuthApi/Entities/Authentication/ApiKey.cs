using Shared.Entities;

namespace AuthApi.Entities.Authentication
{
    public class ApiKey : UuidBaseEntity
    {
        public string Key { get; set; }
    }
}
