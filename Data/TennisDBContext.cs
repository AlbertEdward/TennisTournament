using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Data.Models;

namespace TennisTournament.Data
{
    public class TennisDbContext : IdentityDbContext<ApplicationUser>
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<Player>()
                .HasOne<ApplicationUser>()
                .WithOne()
                .HasForeignKey<Player>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

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