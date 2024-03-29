﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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

        public DbSet<Challenge> Challenges { get; init; }

        public DbSet<Player> Players { get; init; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Round> Rounds { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Player>()
                .HasOne(p => p.User)
                .WithOne(u => u.Player)
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
