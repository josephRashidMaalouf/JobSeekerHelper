using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class ResumeService(IResumeRepository repo) : IResumeService
{
    private readonly IResumeRepository _resumeRepository = repo;
    
    public async Task<Result<Resume>> GetByIdAsync(Guid id)
    {
        return await _resumeRepository.GetByIdAsync(id);
    }

    public async Task<Result<Resume>> AddAsync(Resume entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Resume>> UpdateAsync(Resume entity)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
}