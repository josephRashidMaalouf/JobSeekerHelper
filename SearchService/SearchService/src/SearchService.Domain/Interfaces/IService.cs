using SearchService.Domain.Models;

namespace SearchService.Domain.Interfaces;

public interface IService<T> : ICrud<T>
{
    Task<Result<List<T>>> GetAllByUserIdAsync(Guid userId);
}