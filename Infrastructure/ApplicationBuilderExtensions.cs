using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Data.Models;

using static TennisTournament.Infrastructure.Seeder.Roles;

namespace TennisTournament.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PreparedDatabase(
            this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var service = serviceScope.ServiceProvider;

            MigrateDatabase(service);
            SeedAdministrator(service);

            return app;
        }

        private static void MigrateDatabase(IServiceProvider service)
        {
            var data = service.GetRequiredService<TennisDbContext>();

            data.Database.Migrate();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            Task
                .Run(async () =>
                {
                    if (await roleManager.RoleExistsAsync(Administrator))
                    {
                        return;
                    }

                    var role = new IdentityRole { Name = Administrator };

                    await roleManager.CreateAsync(role);

                    const string adminEmail = "admin@gmail.com";
                    const string adminPassword = "Tennis1234$";

                    var applicationUser = new ApplicationUser
                    {
                        Email = adminEmail,
                        UserName = adminEmail,
                        FullName = "Admin"
                    };

                    await userManager.CreateAsync(applicationUser, adminPassword);

                    await userManager.AddToRoleAsync(applicationUser, role.Name);

                })
            .GetAwaiter()
            .GetResult();
        }
    }
}
