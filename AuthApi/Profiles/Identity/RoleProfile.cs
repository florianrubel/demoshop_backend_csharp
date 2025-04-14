using AuthApi.Entities.Identity;
using AuthApi.Models.Identity.Role;
using LibUniversal.Profiles;

namespace AuthApi.Profiles.Identity
{
    public class RoleProfile : DefaultProfile<Role, ViewRole, CreateRole, PatchRole>
    {
        public RoleProfile()
        {
            CreateMap<Role, ViewRole>();
        }
    }
}
