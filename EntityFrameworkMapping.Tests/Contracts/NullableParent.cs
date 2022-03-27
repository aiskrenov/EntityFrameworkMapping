using Mapster;
using System.Collections.Generic;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(NullableParentEntity), PreserveReference = true), GenerateMapper]
    public class NullableParent
    {
        public int Id { get; set; }

        public List<NullableChild> Children { get; set; } = new();
    }
}
