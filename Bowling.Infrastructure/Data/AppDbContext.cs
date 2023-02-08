using Bowling.Core.Entities;
using Bowling.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Bowling.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Turn> Turns { get; set; }
        public DbSet<Scores> Scores { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new GameConfiguration());
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfiguration(new TurnConfiguration());
            modelBuilder.ApplyConfiguration(new ScoresConfiguration());
        }
    }
}
