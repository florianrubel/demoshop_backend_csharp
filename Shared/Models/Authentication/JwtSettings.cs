namespace Shared.Models.Authentication
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string Key { get; set; }

        public int BearerTTL { get; set; }

        public int RefreshTTL { get; set; }

        public int ApiTokenTTL { get; set; }
    }
}
