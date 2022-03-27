using Mapster;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(MultiRefChildEntity), PreserveReference = true), GenerateMapper]
    public class MultiRefChild
    {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int ReferenceId { get; set; }
    }
}
