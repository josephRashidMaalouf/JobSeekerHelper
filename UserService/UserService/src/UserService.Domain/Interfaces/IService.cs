using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface IService<T> : ICrud<T> where T : EntityWithUserIdBase
{
    Task<Result<List<T>>> GetAllByUserIdAsync(Guid userId);
}