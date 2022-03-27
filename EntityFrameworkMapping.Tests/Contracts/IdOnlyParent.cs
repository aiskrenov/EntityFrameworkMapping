using Mapster;
using System.Collections.Generic;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(IdOnlyParentEntity), PreserveReference = true), GenerateMapper]
    public class IdOnlyParent
    {
        public int Id { get; set; }

        public List<IdOnlyChild> Children { get; set; } = new();
    }
}
