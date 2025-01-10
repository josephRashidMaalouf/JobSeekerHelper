using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using Serilog;
using UserService.Api.Endpoints;
using UserService.Application.Services;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
var dbName = builder.Configuration.GetSection("Database")["Name"] ?? "";

builder.Services.AddScoped<IResumeRepository, ResumeRepository>(provider =>
{
    var collectionName = builder.Configuration.GetSection("Database")["Resumes"] ?? "";
    return new ResumeRepository(collectionName, connectionString, dbName);
});
builder.Services.AddScoped<ISearchSettingsRepository, SearchSettingsRepository>(provider =>
{
    var collectionName = builder.Configuration.GetSection("Database")["SearchSettings"] ?? "";
    return new SearchSettingsRepository(collectionName, connectionString, dbName);
});

builder.Services.AddScoped<ISearchSettingsService, SearchSettingsService>();
builder.Services.AddScoped<IResumeService, ResumeService>();


builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    // Configure here Serilog instance...
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.WithProperty("ApplicationContext", "UserService.Api")
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("UserService"))
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
            .AddHttpClientInstrumentation();

        tracing.AddOtlpExporter();
    });


builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());


var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithApiKeyAuthentication(keyOptions => keyOptions.Token = "apikey");
});

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();