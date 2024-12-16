using AutoMapper;

namespace Shared.Profiles
{
    public abstract class DefaultProfile<EntityType, ViewType, CreateType, PatchType> : Profile
    {
        public DefaultProfile()
        {
            CreateMap<EntityType, ViewType>();
            CreateMap<CreateType, EntityType>();
            CreateMap<PatchType, EntityType>().ReverseMap();
        }
    }
}
