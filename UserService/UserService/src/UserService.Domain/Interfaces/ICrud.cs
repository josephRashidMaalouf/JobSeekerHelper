using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface ICrud<TEntity> where TEntity : EntityBase
{
    Task<Result<TEntity>> GetByIdAsync(Guid id);
    Task<Result<TEntity>> AddAsync(TEntity entity);
    Task<Result<TEntity>> UpdateAsync(TEntity entity);
    Task<Result<Guid>> DeleteAsync(Guid id);
}