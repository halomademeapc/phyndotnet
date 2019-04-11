using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhyndData.Entities
{
    public class Game
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool? WasWon { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
    }
}
