using Mapster;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(CircularChild), PreserveReference = true), GenerateMapper]
    public class CircularParentEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public List<CircularChildEntity> Children { get; set; } = new();
    }
}
