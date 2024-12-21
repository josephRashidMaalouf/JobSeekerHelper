using AuthService.Api.ExtensionMethods.Endpoints;
using AuthService.Infrastructure.Entities;
using AuthService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
 
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddEndpointsApiExplorer().AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthorization();

builder.Services.AddAuthentication();

builder.Services.AddIdentityApiEndpoints<User>()
    .AddRoles<Role>()
    .AddEntityFrameworkStores<AppDbContext>();


builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    // Configure here Serilog instance...
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.WithProperty("ApplicationContext", "AuthService.Api")
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("AuthService"))
    .WithMetrics(metrics =>
    {
        metrics
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation();

        metrics.AddOtlpExporter();
    })
    .WithTracing(tracing =>
    {
        tracing
            .AddAspNetCoreInstrumentation()
            .AddHttpClientInstrumentation()
            .AddEntityFrameworkCoreInstrumentation();

        tracing.AddOtlpExporter();
    });



builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());

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