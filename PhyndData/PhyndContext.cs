using Microsoft.EntityFrameworkCore;
using PhyndData.Entities;

namespace PhyndData
{
    public class PhyndContext : DbContext
    {
        public PhyndContext(DbContextOptions<PhyndContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Move> Moves { get; set; }
        public DbSet<Weight> Weights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weight>()
                .HasKey(c => new { c.Scenario, c.NextMove });
        }
    }
}
