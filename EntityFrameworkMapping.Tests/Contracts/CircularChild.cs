using Mapster;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(CircularChildEntity), PreserveReference = true), GenerateMapper]
    public class CircularChild
    {
        public int Id { get; set; }

        public CircularParent Parent { get; set; }
    }
}
