using Mapster;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(IdOnlyChildEntity), PreserveReference = true), GenerateMapper]
    public class IdOnlyChild
    {
        public int Id { get; set; }

        public int ParentId { get; set; }
    }
}
