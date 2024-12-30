using FakeItEasy;
using UserService.Application.Services;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.EmailModelTests.ServiceTests;

public class SearchSettingsTests
{
    private readonly ISearchSettingsService _sut;
    private readonly ISearchSettingsRepository _searchSettingsRepository;
    public SearchSettingsTests()
    {
        _searchSettingsRepository = A.Fake<ISearchSettingsRepository>();
        _sut = new SearchSettingsService(_searchSettingsRepository);
    }
    [Fact]
    public async Task GetByIdAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        var dummySearchSettings = A.Dummy<SearchSettings>();
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._))
            .Returns(Result<SearchSettings>.Success(dummySearchSettings));
        
        
        //Act 
        var result = await _sut.GetByIdAsync(A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<SearchSettings>>(result);
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task GetAllByIdAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetAllByUserIdAsync(A<Guid>._))
            .Returns(Result<List<SearchSettings>>.Success(A.Dummy<List<SearchSettings>>()));
        
        
        //Act 
        var result = await _sut.GetAllByUserIdAsync(A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<List<SearchSettings>>>(result);
        A.CallTo(() => _searchSettingsRepository.GetAllByUserIdAsync(A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task AddAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.AddAsync(A<SearchSettings>._))
            .Returns(Result<SearchSettings>.Success(A.Dummy<SearchSettings>()));
        
        //Act
        var result = await _sut.AddAsync(A.Dummy<SearchSettings>());
        
        //Assert
        Assert.IsType<Result<SearchSettings>>(result);
        A.CallTo(() => _searchSettingsRepository.AddAsync(A<SearchSettings>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task UpdateAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._))
            .Returns(Result<SearchSettings>.Success(A.Dummy<SearchSettings>()));
        
        //Act
        var result = await _sut.UpdateAsync(A.Dummy<SearchSettings>());
        
        //Assert
        Assert.IsType<Result<SearchSettings>>(result);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._))
            .MustHaveHappenedOnceExactly();
    }
    
    [Fact]
    public async Task DeleteAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.DeleteAsync(A<Guid>._))
            .Returns(Result<Guid>.Success(A.Dummy<Guid>()));
        
        //Act
        var result = await _sut.DeleteAsync(A.Dummy<Guid>());
        
        //Assert
        Assert.IsType<Result<Guid>>(result);
        A.CallTo(() => _searchSettingsRepository.DeleteAsync(A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }
}