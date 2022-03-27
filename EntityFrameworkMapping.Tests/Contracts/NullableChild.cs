using Mapster;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(NullableChildEntity), PreserveReference = true), GenerateMapper]
    public class NullableChild
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }
    }
}
