using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class SearchSettingsService : ISearchSettingsService
{
    public async Task<Result<SearchSettings>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SearchSettings>> AddAsync(SearchSettings entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<SearchSettings>> UpdateAsync(SearchSettings entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}