using Mapster;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(CircularChild), PreserveReference = true), GenerateMapper]
    public class CircularChildEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public CircularParentEntity Parent { get; set; }
    }
}
