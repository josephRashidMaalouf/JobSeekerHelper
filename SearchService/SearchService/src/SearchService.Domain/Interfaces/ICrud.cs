using SearchService.Domain.Models;

namespace SearchService.Domain.Interfaces;

public interface ICrud<TEntity>
{
    Task<Result<TEntity>> GetByIdAsync(Guid id, Guid userId);
    Task<Result<TEntity>> AddAsync(TEntity entity, Guid userId);
    Task<Result<TEntity>> UpdateAsync(TEntity entity, Guid userId);
    Task<Result<Guid>> DeleteAsync(Guid id, Guid userId);
}