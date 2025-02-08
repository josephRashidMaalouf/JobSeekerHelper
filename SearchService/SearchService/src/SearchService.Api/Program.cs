using JobSeekerHelper.Nuget.Extensions;
using SearchService.Api.Endpoints;
using SearchService.Application.Services;
using SearchService.Domain.Interfaces;
using SearchService.Infrastructure.Repositories;

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

builder.SetUpMicroService("SearchService");

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapOpenApi();

app.SetUpMicroService();

app.MapSearchSettingsEndpoints();

app.UseHttpsRedirection();


app.Run();