using Mapster;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(MultiRefChild), PreserveReference = true), GenerateMapper]
    public class MultiRefChildEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ParentId { get; set; }

        public int ReferenceId { get; set; }

        public MultiRefParentEntity Parent { get; set; }

        public MultiRefReferenceEntity Reference { get; set; }
    }
}
