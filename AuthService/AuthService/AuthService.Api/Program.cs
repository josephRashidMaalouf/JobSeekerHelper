using AuthService.Api.ExtensionMethods;
using AuthService.Api.ExtensionMethods.Endpoints;
using AuthService.Infrastructure.Entities;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();

builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("TestConnection") ?? "";

builder.Services.AddEndpointsApiExplorer().AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization();

builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithApiKeyAuthentication(keyOptions => keyOptions.Token = "apikey");
});

app.MapIdentityApi<User>();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapIdentityEndpoints();

ApplyMigration();
app.Run();



void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (_db.Database.GetPendingMigrations().Any())
        {
            _db.Database.Migrate();
        }
    }
}

