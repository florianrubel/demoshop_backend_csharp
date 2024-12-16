namespace Shared.Constants
{
    public static class Authentication
    {
        public const string CLAIM_SCOPE = "scope";
        public const string CLAIM_DEVICE_ID = "deviceid";

        public static readonly List<string> CUSTOM_CLAIMS = new List<string>
        {
            CLAIM_SCOPE
        };
    }
}
