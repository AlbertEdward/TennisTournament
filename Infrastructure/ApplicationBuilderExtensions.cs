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
            if (data.TypeOfGames.Any())
            {
                return;
            }

            data.TypeOfGames.AddRange(new[]
            {
                new TypeOfGame { Name = "Singles"},
                new TypeOfGame { Name = "Doubles"},
            });

            data.SaveChanges();
        }
    }
}
