using Mapster;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(MultiRefChild), PreserveReference = true), GenerateMapper]
    public class MultiRefParentEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ReferenceId { get; set; }

        public List<MultiRefChildEntity> Children { get; set; } = new();

        public MultiRefReferenceEntity Reference { get; set; }
    }
}
