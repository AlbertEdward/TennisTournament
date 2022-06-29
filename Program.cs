using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TennisTournament.Data;
using TennisTournament.Infrastructure;
using TennisTournament.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionStrings:TennisSqlServer"];

builder.Services.AddDbContext<TennisDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<TennisDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllersWithViews(options
    => options.Filters.Add<AutoValidateAntiforgeryTokenAttribute>());

builder.Services.AddAuthentication()
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
        facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
    });

builder.Services.AddMemoryCache();
builder.Services.AddServices();

//builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.Jwt));

//builder.Services.AddJwtAuthentication(builder.Configuration);

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