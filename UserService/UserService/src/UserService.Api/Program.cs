using JobSeekerHelper.Nuget.Extensions;
using UserService.Api.Endpoints;
using UserService.Application.Services;
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

builder.Services.AddScoped<IResumeService, ResumeService>();

builder.SetUpMicroService(builder.Configuration["ServiceName"] ?? "UserService");



var app = builder.Build();

app.MapOpenApi();

app.SetUpMicroService();

app.UseHttpsRedirection();

app.MapEndpoints();

app.Run();