using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks.API.Model.Domain
{
    [Table("Difficulty")]
    public class Difficulty
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
