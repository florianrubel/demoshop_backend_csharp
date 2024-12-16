using AuthApi.Entities.Identity;
using AuthApi.Models.Identity.Role;
using AutoMapper;

namespace AuthApi.Profiles.Identity
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, ViewRole>();
        }
    }
}
