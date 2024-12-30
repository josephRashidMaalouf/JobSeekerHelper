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
        return await _resumeRepository.AddAsync(entity);
    }

    public async Task<Result<Resume>> UpdateAsync(Resume entity)
    {
        return await _resumeRepository.UpdateAsync(entity);
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id)
    {
        return await _resumeRepository.DeleteAsync(id);
    }

    public async Task<Result<List<Resume>>> GetAllByUserIdAsync(Guid userId)
    {
        return await _resumeRepository.GetAllByUserIdAsync(userId);
    }
}