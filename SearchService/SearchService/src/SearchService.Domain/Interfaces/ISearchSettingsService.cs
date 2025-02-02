using JobSeekerHelper.Nuget.Interfaces;
using JobSeekerHelper.Nuget.Results;
using SearchService.Domain.Entities;

namespace SearchService.Domain.Interfaces;

public interface ISearchSettingsService : IService<SearchSettings>
{
    Task<Result<bool>> StartSearchAsync(Guid settingsId, Guid userId);
    Task<Result<bool>> StopSearchAsync(Guid settingsId, Guid userId);
}