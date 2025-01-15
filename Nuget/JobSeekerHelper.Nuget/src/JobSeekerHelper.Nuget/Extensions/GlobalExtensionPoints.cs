using JobSeekerHelper.Nuget.Converters;
using JobSeekerHelper.Nuget.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Microsoft.AspNetCore.Builder;
using Scalar.AspNetCore;

namespace JobSeekerHelper.Nuget.Extensions;

public static class GlobalExtensionPoints
{
    /// <summary>
    /// Set up microservice with common settings
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="serviceName">Name of the micro-service using this extension in order to set up logging</param>
    /// <returns></returns>
    public static WebApplicationBuilder SetUpMicroService(this WebApplicationBuilder builder, string serviceName)
    {
        builder.Services.Configure<JsonOptions>(options =>
        {
            options.SerializerOptions.Converters.Add(new GuidConverter());
            options.SerializerOptions.Converters.Add(new DateOnlyConverter());
        });


        builder.Host.UseSerilog((context, services, loggerConfiguration) =>
        {
            // Configure here Serilog instance...
            loggerConfiguration
                .MinimumLevel.Information()
                .Enrich.WithProperty("ApplicationContext", serviceName)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(context.Configuration);
        });


        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(serviceName))
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

        return builder;
    }

    /// <summary>
    /// Configure application for common things, for example providing metrics and configuration debug tool
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static WebApplication SetUpMicroService(this WebApplication app)
    {
        
        app.MapScalarApiReference(options =>
        {
            options
                .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
                .WithApiKeyAuthentication(keyOptions => keyOptions.Token = "apikey");
        });
        return app;
    }
}