using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;

namespace TennisTournament.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();

            var data = scopedServices.ServiceProvider.GetService<TennisDbContext>();

            data.Database.Migrate();

            SeedTypes(data);

            return app;
        }

        private static void SeedTypes(TennisDbContext data)
        {
            if (data.GameTypes.Any())
            {
                return;
            }

            data.GameTypes.AddRange(new[]
            {
                new GameType { Name = "Singles"},
                new GameType { Name = "Doubles"},
            });

            if (data.CourtTypes.Any())
            {
                return;
            }

            data.CourtTypes.AddRange(new[]
            {
                new CourtType { Name = "Clay"},
                new CourtType { Name = "Omni"},
                new CourtType { Name = "Hard"},
                new CourtType { Name = "Floor"}
            });

            if (data.Sets.Any())
            {
                return;
            }

            data.Sets.AddRange(new[]
            {
                new Set { Name = "1"},
                new Set { Name = "3"},
                new Set { Name = "5"},
            });

            if (data.Games.Any())
            {
                return;
            }

            data.Games.AddRange(new[]
            {
                new Game { Name = "4"},
                new Game { Name = "6"},
                new Game { Name = "8"},
            });

            if (data.Rules.Any())
            {
                return;
            }

            data.Rules.AddRange(new[]
            {
                new Rule { Name = "Tie-Break"},
                new Rule { Name = "First"},
            });

            if (data.LastSets.Any())
            {
                return;
            }

            data.LastSets.AddRange(new[]
            {
                new LastSet { Name = "Normal"},
                new LastSet { Name = "Advantage (2G)"},
                new LastSet { Name = "Match Tie-Break (7P)"},
                new LastSet { Name = "Match Tie-Break (10P)"},
            });

            data.SaveChanges();
        }
    }
}
