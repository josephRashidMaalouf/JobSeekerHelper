using JobSeekerHelper.Nuget.Results;
using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;

namespace SearchService.Application.Services;

public class SearchSettingsService(ISearchSettingsRepository repo) : ISearchSettingsService
{
    private readonly ISearchSettingsRepository _searchSettingsRepository = repo;
    public async Task<Result<SearchSettings>> GetByIdAsync(Guid id, Guid userId)
    {
        return await _searchSettingsRepository.GetByIdAsync(id, userId);
    }

    public async Task<Result<SearchSettings>> AddAsync(SearchSettings entity, Guid userId)
    {
        return await _searchSettingsRepository.AddAsync(entity, userId);
    }

    public async Task<Result<SearchSettings>> UpdateAsync(SearchSettings entity, Guid userId)
    {
        return await _searchSettingsRepository.UpdateAsync(entity, userId);
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id, Guid userId)
    {
        return await _searchSettingsRepository.DeleteAsync(id, userId);
    }

    public async Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId)
    {
        return await _searchSettingsRepository.GetAllByUserIdAsync(userId);
    }

    public async Task<Result<bool>> StartSearchAsync(Guid settingsId, Guid userId)
    {
        var result = await _searchSettingsRepository.GetByIdAsync(settingsId, userId);
        if (!result.IsSuccess)
        {
            return Result<bool>.Failure(result.ErrorMessage ?? "Something went wrong", result.Code);
        }
        result.Data!.IsActive = true;
        
        var updateResult = await _searchSettingsRepository.UpdateAsync(result.Data, userId);
        
        if (!updateResult.IsSuccess)
        {
            return Result<bool>.Failure(result.ErrorMessage ?? "Something went wrong", result.Code);
        }
        
        return Result<bool>.Success(true);

    }

    public async Task<Result<bool>> StopSearchAsync(Guid settingsId, Guid userId)
    {
        throw new NotImplementedException();
    }
}