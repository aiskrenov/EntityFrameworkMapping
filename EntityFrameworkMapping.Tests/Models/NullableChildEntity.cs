using Mapster;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(NullableChild), PreserveReference = true), GenerateMapper]
    public class NullableChildEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public NullableParentEntity Parent { get; set; }
    }
}
