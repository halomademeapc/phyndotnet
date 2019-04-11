using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhyndData.Entities
{
    public class Game
    {
        [Key] public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
        public bool? WasWon { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
    }
}
