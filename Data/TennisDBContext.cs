using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Data.Models;

namespace TennisTournament.Data
{
    public class TennisDbContext : IdentityDbContext<IdentityUser>
    {
        public TennisDbContext()
        {
        }

        public TennisDbContext(DbContextOptions<TennisDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tournament> Tournaments { get; init; }

        public DbSet<Player> Players { get; init; }

        public DbSet<GameType> GameTypes { get; init; }

        public DbSet<CourtType> CourtTypes { get; init; }

        public DbSet<Set> Sets { get; init; }

        public DbSet<Game> Games { get; init; }

        public DbSet<Rule> Rules { get; init; }

        public DbSet<LastSet> LastSets { get; init; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=TennisTournaments;Integrated Security=True;");
            }
        }
    }
}