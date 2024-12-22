using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface ISearchSettingsRepository : ICrud<SearchSettings>
{
    Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId);
}