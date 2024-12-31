using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Api.Endpoints;

public static class ResumeEndpoints
{
    public static IEndpointRouteBuilder MapResumeEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user/{userId:guid}/resume");

        group.MapGet("", GetAllByUserIdAsync);
        group.MapGet("/{resumeId:guid}", GetByResumeIdAsync);

        return app;
    }

    private static async Task<IResult> GetAllByUserIdAsync(IResumeService service, Guid userId)
    {
        var result = await service.GetAllByUserIdAsync(userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }
        
        return Results.Ok(result.Data);
    }

    private static async Task<IResult> GetByResumeIdAsync(IResumeService service, Guid resumeId, Guid userId)
    {
        var result = await service.GetByIdAsync(resumeId, userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }
        
        return Results.Ok(result.Data);
    }

    private static async Task<IResult> PostAsync(IResumeService service, Guid userId, Resume resume)
    {
        var result = await service.AddAsync(resume, userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }
        
        return Results.Ok(result.Data);
    }
    
}