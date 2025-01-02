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
    return new ResumeRepository(connectionString, dbName, collectionName);
});
builder.Services.AddScoped<ISearchSettingsRepository, SearchSettingsRepository>(provider =>
{
    var collectionName = builder.Configuration.GetSection("Database")["SearchSettingsCollection"] ?? "";
    return new SearchSettingsRepository(connectionString, dbName, collectionName);
});

builder.Services.AddScoped<ISearchSettingsService, SearchSettingsService>();
builder.Services.AddScoped<IResumeService, ResumeService>();

var app = builder.Build();


app.MapOpenApi();


app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();