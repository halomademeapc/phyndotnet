using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhyndData.Entities
{
    public class Move
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid GameId { get; set; }
        public Player Player { get; set; }
        public int Position { get; set; }

        public virtual Game Game { get; set; }
    }
}
