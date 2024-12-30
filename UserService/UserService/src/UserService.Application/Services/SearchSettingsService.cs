using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class SearchSettingsService(ISearchSettingsRepository repo) : ISearchSettingsService
{
    private readonly ISearchSettingsRepository _searchSettingsRepository = repo;
    public async Task<Result<SearchSettings>> GetByIdAsync(Guid id)
    {
        return await _searchSettingsRepository.GetByIdAsync(id);
    }

    public async Task<Result<SearchSettings>> AddAsync(SearchSettings entity)
    {
        return await _searchSettingsRepository.AddAsync(entity);
    }

    public async Task<Result<SearchSettings>> UpdateAsync(SearchSettings entity)
    {
        return await _searchSettingsRepository.UpdateAsync(entity);
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return await _searchSettingsRepository.DeleteAsync(id);
    }

    public async Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId)
    {
        return await _searchSettingsRepository.GetAllByUserIdAsync(userId);
    }
}