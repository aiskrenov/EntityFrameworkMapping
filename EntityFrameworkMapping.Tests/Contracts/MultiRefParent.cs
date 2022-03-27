using Mapster;
using System.Collections.Generic;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(MultiRefParentEntity), PreserveReference = true), GenerateMapper]
    public class MultiRefParent
    {
        public int Id { get; set; }

        public int ReferenceId { get; set; }

        public List<MultiRefChild> Children { get; set; } = new();
    }
}
