using AutoMapper;

namespace EntityFrameworkMapping.Tests
{
    public class IdOnlyMapperProfiles : Profile
    {
        public IdOnlyMapperProfiles()
        {
            CreateMap<IdOnlyParent, IdOnlyParentEntity>()
                .ReverseMap();

            CreateMap<IdOnlyChild, IdOnlyChildEntity>()
                .ReverseMap();
        }
    }
}
