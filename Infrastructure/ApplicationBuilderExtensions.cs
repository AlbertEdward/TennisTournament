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

            data.SaveChanges();
        }
    }
}
