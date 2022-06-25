using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Infrastructure;
using TennisTournament.Data;
using Microsoft.AspNetCore.Mvc;
using TennisTournament.Services.Statistics;
using TennisTournament.Services.Tournaments;
using TennisTournament.Services.Players;
using TennisTournament.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = @"Server=.;Database=TennisTournaments;Integrated Security=True;";

builder.Services.AddDbContext<TennisDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<TennisDbContext>();

builder.Services.AddControllersWithViews(options
    => options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>());

builder.Services.AddTransient<IStatisticsService, StatisticsService>();
builder.Services.AddTransient<ITournamentService, TournamentService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IUploadFileService, UploadFileService>();

var app = builder.Build();

app.PrepareDatabase();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app
    .UseHttpsRedirection()
    .UseStaticFiles()
    .UseRouting()
    .UseAuthentication()
    .UseAuthorization()
    .UseEndpoints(endpoints =>
    {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapRazorPages();
    });

app.Run();