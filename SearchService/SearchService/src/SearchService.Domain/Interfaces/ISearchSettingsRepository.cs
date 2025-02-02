using JobSeekerHelper.Nuget.Interfaces;
using JobSeekerHelper.Nuget.Results;
using SearchService.Domain.Entities;

namespace SearchService.Domain.Interfaces;

public interface ISearchSettingsRepository : ICrud<SearchSettings>
{
    Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId);
}