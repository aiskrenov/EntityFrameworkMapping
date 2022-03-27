using Mapster;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(MultiRefReferenceEntity), PreserveReference = true), GenerateMapper]
    public class MultiRefReference
    {
        public int Id { get; set; }
    }
}
