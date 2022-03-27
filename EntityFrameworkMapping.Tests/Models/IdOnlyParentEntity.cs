using Mapster;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(IdOnlyParent), PreserveReference = true), GenerateMapper]
    public class IdOnlyParentEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public List<IdOnlyChildEntity> Children { get; set; } = new();
    }
}
