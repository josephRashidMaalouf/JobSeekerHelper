using FakeItEasy;
using UserService.Application.Services;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.EmailModelTests.ServiceTests;

public class ResumeSettingsTests
{
    private readonly IResumeService _sut;
    private readonly IResumeRepository _resumeRepository;
    public ResumeSettingsTests()
    {
        _resumeRepository = A.Fake<IResumeRepository>();
        _sut = new ResumeService(_resumeRepository);
    }
    [Fact]
    public async Task GetByIdAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        var dummyResume = A.Dummy<Resume>();
        A.CallTo(() => _resumeRepository.GetByIdAsync(A<Guid>._))
            .Returns(Result<Resume>.Success(dummyResume));
        
        
        //Act 
        var result = await _sut.GetByIdAsync(A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<Resume>>(result);
        A.CallTo(() => _resumeRepository.GetByIdAsync(A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task GetAllByIdAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _resumeRepository.GetAllByUserIdAsync(A<Guid>._))
            .Returns(Result<List<Resume>>.Success(A.Dummy<List<Resume>>()));
        
        
        //Act 
        var result = await _sut.GetAllByUserIdAsync(A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<List<Resume>>>(result);
        A.CallTo(() => _resumeRepository.GetAllByUserIdAsync(A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _resumeRepository.AddAsync(A<Resume>._))
            .Returns(Result<Resume>.Success(A.Dummy<Resume>()));
        
        //Act
        var result = await _sut.AddAsync(A.Dummy<Resume>());
        
        //Assert
        Assert.IsType<Result<Resume>>(result);
        A.CallTo(() => _resumeRepository.AddAsync(A<Resume>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task UpdateAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _resumeRepository.UpdateAsync(A<Resume>._))
            .Returns(Result<Resume>.Success(A.Dummy<Resume>()));
        
        //Act
        var result = await _sut.UpdateAsync(A.Dummy<Resume>());
        
        //Assert
        Assert.IsType<Result<Resume>>(result);
        A.CallTo(() => _resumeRepository.UpdateAsync(A<Resume>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task DeleteAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _resumeRepository.DeleteAsync(A<Guid>._))
            .Returns(Result<Guid>.Success(A.Dummy<Guid>()));
        
        //Act
        var result = await _sut.DeleteAsync(A.Dummy<Guid>());
        
        //Assert
        Assert.IsType<Result<Resume>>(result);
        A.CallTo(() => _resumeRepository.DeleteAsync(A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }
}