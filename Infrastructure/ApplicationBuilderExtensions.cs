using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;

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

            return app;
        }
    }
}
