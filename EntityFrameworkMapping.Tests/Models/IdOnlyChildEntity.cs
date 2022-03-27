using Mapster;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(IdOnlyChild), PreserveReference = true), GenerateMapper]
    public class IdOnlyChildEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ParentId { get; set; }

        public IdOnlyParentEntity Parent { get; set; }
    }
}
