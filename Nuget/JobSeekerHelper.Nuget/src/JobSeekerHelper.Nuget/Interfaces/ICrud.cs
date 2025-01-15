using JobSeekerHelper.Nuget.Entities;
using JobSeekerHelper.Nuget.Results;

namespace JobSeekerHelper.Nuget.Interfaces;

public interface ICrud<TEntity> where TEntity : EntityWithUserIdBase
{
    Task<Result<TEntity>> GetByIdAsync(Guid id, Guid userId);
    Task<Result<TEntity>> AddAsync(TEntity entity, Guid userId);
    Task<Result<TEntity>> UpdateAsync(TEntity entity, Guid userId);
    Task<Result<Guid>> DeleteAsync(Guid id, Guid userId);
}