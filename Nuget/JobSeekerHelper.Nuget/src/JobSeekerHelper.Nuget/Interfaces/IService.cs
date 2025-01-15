using JobSeekerHelper.Nuget.Entities;
using JobSeekerHelper.Nuget.Results;

namespace JobSeekerHelper.Nuget.Interfaces;

public interface IService<T> : ICrud<T> where T : EntityWithUserIdBase
{
    Task<Result<List<T>>> GetAllByUserIdAsync(Guid userId);
}