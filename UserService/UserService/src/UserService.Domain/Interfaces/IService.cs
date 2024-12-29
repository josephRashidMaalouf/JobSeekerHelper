using UserService.Domain.Entities;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces;

public interface IService<T> : ICrud<T> where T : EntityBase
{
    Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId);
}