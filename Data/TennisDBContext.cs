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

        public DbSet<TypeOfGame> TypeOfGames { get; init; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=.;Database=TennisTournaments;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}