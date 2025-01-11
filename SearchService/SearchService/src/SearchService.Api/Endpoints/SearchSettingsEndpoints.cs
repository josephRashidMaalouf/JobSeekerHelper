using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;

namespace SearchService.Api.Endpoints;

public static class SearchSettingsEndpoints
{
    public static IEndpointRouteBuilder MapSearchSettingsEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/user/{userId:guid}/searchSettings");

        group.MapGet("", GetAllByUserIdAsync);
        group.MapGet("/{searchSettingsId:guid}", GetBySearchSettingsIdAsync);
        group.MapPost("", PostAsync);
        group.MapPut("", UpdateAsync);
        group.MapDelete("", DeleteAsync);

        return group;
    }

    private static async Task<IResult> GetAllByUserIdAsync(ISearchSettingsService service, Guid userId)
    {
        var result = await service.GetAllByUserIdAsync(userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }

        return Results.Ok(result.Data);
    }

    private static async Task<IResult> GetBySearchSettingsIdAsync(ISearchSettingsService service, Guid searchSettingsId, Guid userId)
    {
        var result = await service.GetByIdAsync(searchSettingsId, userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }

        return Results.Ok(result.Data);
    }

    private static async Task<IResult> PostAsync(ISearchSettingsService service, Guid userId, SearchSettings searchSettings)
    {
        var result = await service.AddAsync(searchSettings, userId);

        if (!result.IsSuccess)
        {
            Results.BadRequest(result.ErrorMessage);
        }

        return Results.Ok(result.Data);
    }
    private static async Task<IResult> UpdateAsync(ISearchSettingsService service, Guid userId, SearchSettings searchSettings)
    {
        var result = await service.UpdateAsync(searchSettings, userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }

        return Results.Ok(result.Data);
    }
    private static async Task<IResult> DeleteAsync(ISearchSettingsService service, Guid userId, Guid searchSettingsId)
    {
        var result = await service.DeleteAsync(searchSettingsId, userId);

        if (!result.IsSuccess)
        {
            return result.Code == 404 ? Results.NotFound(result.ErrorMessage) : Results.BadRequest(result.ErrorMessage);
        }

        return Results.Ok(result.Data);
    }
}