using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Application.Services;

public class ResumeService(IResumeRepository repo) : IResumeService
{
    private readonly IResumeRepository _resumeRepository = repo;
    
    public async Task<Result<Resume>> GetByIdAsync(Guid id, Guid userId)
    {
        return await _resumeRepository.GetByIdAsync(id, userId);
    }

    public async Task<Result<Resume>> AddAsync(Resume entity, Guid userId)
    {
        return await _resumeRepository.AddAsync(entity, userId);
    }

    public async Task<Result<Resume>> UpdateAsync(Resume entity, Guid userId)
    {
        return await _resumeRepository.UpdateAsync(entity, userId);
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id, Guid userId)
    {
        return await _resumeRepository.DeleteAsync(id, userId);
    }

    public async Task<Result<List<Resume>>> GetAllByUserIdAsync(Guid userId)
    {
        return await _resumeRepository.GetAllByUserIdAsync(userId);
    }
}