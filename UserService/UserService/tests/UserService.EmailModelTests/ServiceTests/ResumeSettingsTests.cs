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

    public async Task AddAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _resumeRepository.AddAsync(A<Resume>._))
            .Returns(Result<Resume>.Success(A.Dummy<Resume>()));
        
        //Act
        
    }
}