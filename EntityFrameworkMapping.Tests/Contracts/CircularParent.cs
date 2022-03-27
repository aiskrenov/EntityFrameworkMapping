using Mapster;
using System.Collections.Generic;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(CircularParentEntity), PreserveReference = true), GenerateMapper]
    public class CircularParent
    {
        public int Id { get; set; }

        public List<CircularChild> Children { get; set; } = new();
    }
}
