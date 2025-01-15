using JobSeekerHelper.Nuget.Interfaces;
using JobSeekerHelper.Nuget.Results;
using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface IResumeRepository : ICrud<Resume>
{
    Task<Result<List<Resume>>> GetAllByUserIdAsync(Guid userId);
}