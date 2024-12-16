namespace Shared.Constants
{
    public static class Identity
    {
        public const string ROLE_SUPERADMIN = "Superadmin";
        public const string ROLE_ADMIN = "Admin";

        public const string AUTHORIZE_MIN_ADMIN = $"{ROLE_SUPERADMIN},{ROLE_SUPERADMIN}";

        public static readonly List<string> ROLES = new List<string>
        {
            ROLE_SUPERADMIN,
            ROLE_ADMIN
        };
    }
}
