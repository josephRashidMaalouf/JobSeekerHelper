using Microsoft.AspNetCore.Http.Json;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Scalar.AspNetCore;
using SearchService.Api.Converters;
using SearchService.Api.Endpoints;
using SearchService.Application.Services;
using SearchService.Domain.Interfaces;
using SearchService.Infrastructure.Repositories;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
var dbName = builder.Configuration.GetSection("Database")["Name"] ?? "";


builder.Services.AddScoped<ISearchSettingsRepository, SearchSettingsRepository>(provider =>
{
    var collectionName = builder.Configuration.GetSection("Database")["SearchSettings"] ?? "";
    return new SearchSettingsRepository(collectionName, connectionString, dbName);
});

builder.Services.AddScoped<ISearchSettingsService, SearchSettingsService>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new GuidConverter());
});


builder.Host.UseSerilog((context, services, loggerConfiguration) =>
{
    // Configure here Serilog instance...
    loggerConfiguration
        .MinimumLevel.Information()
        .Enrich.WithProperty("ApplicationContext", "SearchService.Api")
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
        .ReadFrom.Configuration(context.Configuration);
});


builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService("SearchService"))
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

// Configure the HTTP request pipeline.

app.MapOpenApi();
app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithApiKeyAuthentication(keyOptions => keyOptions.Token = "apikey");
});

app.MapSearchSettingsEndpoints();

app.UseHttpsRedirection();


app.Run();