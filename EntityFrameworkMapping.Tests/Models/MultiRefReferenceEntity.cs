using Mapster;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkMapping.Tests
{
    [AdaptTo(typeof(MultiRefReference), PreserveReference = true), GenerateMapper]
    public class MultiRefReferenceEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
    }
}
