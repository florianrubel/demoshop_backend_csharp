using AuthApi.Entities.Identity;
using AuthApi.Models.Identity.User;
using AutoMapper;

namespace AuthApi.Profiles.Identity
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ViewUser>();
        }
    }
}
