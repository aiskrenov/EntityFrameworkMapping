using AutoMapper;

namespace EntityFrameworkMapping.Tests
{
    public class MultiRefMapperProfiles : Profile
    {
        public MultiRefMapperProfiles()
        {
            CreateMap<MultiRefParent, MultiRefParentEntity>()
                .ReverseMap();

            CreateMap<MultiRefChild, MultiRefChildEntity>()
                .ReverseMap();
        }
    }
}
