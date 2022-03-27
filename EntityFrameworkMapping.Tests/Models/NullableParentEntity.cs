using Mapster;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(NullableParent), PreserveReference = true), GenerateMapper]
    public class NullableParentEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public List<NullableChildEntity> Children { get; set; } = new();
    }
}
