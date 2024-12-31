using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

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
}