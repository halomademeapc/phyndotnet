using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhyndData.Entities
{
    public class Weight
    {
        [NotMapped]
        const int BOARD_SIZE = 9;

        [Required, MaxLength(BOARD_SIZE), MinLength(BOARD_SIZE)]
        public string Scenario { get; set; }
        [Range(0, BOARD_SIZE)]
        public int NextMove { get; set; }
        public float Rank { get; set; }
    }
}
