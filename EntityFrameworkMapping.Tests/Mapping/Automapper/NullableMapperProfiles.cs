using AutoMapper;

namespace EntityFrameworkMapping.Tests
{
    public class NullableMapperProfiles : Profile
    {
        public NullableMapperProfiles()
        {
            CreateMap<NullableParent, NullableParentEntity>()
                .ReverseMap();

            CreateMap<NullableChild, NullableChildEntity>()
                .ReverseMap();
        }
    }
}
