using AutoMapper;

namespace EntityFrameworkMapping.Tests
{
    public class CircularMapperProfiles : Profile
    {
        public CircularMapperProfiles()
        {
            CreateMap<CircularParent, CircularParentEntity>()
                .ReverseMap();

            CreateMap<CircularChild, CircularChildEntity>()
                .ReverseMap();
        }
    }
}
