﻿using FakeItEasy;
using JobSeekerHelper.Nuget.Results;
using SearchService.Application.Services;
using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;

namespace SearchService.UnitTests.ServiceTests;

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
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Success(dummySearchSettings));


        //Act 
        var result = await _sut.GetByIdAsync(A.Dummy<Guid>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<SearchSettings>>(result);
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._, A<Guid>._))
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
        A.CallTo(() => _searchSettingsRepository.AddAsync(A<SearchSettings>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Success(A.Dummy<SearchSettings>()));

        //Act
        var result = await _sut.AddAsync(A.Dummy<SearchSettings>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<SearchSettings>>(result);
        A.CallTo(() => _searchSettingsRepository.AddAsync(A<SearchSettings>._, A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Success(A.Dummy<SearchSettings>()));

        //Act
        var result = await _sut.UpdateAsync(A.Dummy<SearchSettings>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<SearchSettings>>(result);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteAsync_CallsRepositoryAndReturnsResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.DeleteAsync(A<Guid>._, A<Guid>._))
            .Returns(Result<Guid>.Success(A.Dummy<Guid>()));

        //Act
        var result = await _sut.DeleteAsync(A.Dummy<Guid>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<Guid>>(result);
        A.CallTo(() => _searchSettingsRepository.DeleteAsync(A<Guid>._, A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task StartSearchAsync_EntityNotFound_ReturnsFalseBoolResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Failure("Not found", 404));

        //Act
        var result = await _sut.StartSearchAsync(A.Dummy<Guid>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<bool>>(result);
        Assert.False(result.IsSuccess);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .MustNotHaveHappened();
    }
    [Fact]
    public async Task StartSearchAsync_UpdateUnsuccessful_ReturnsFalseBoolResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Success(A.Dummy<SearchSettings>()));

        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Failure("Could not update", 404));

        //Act
        var result = await _sut.StartSearchAsync(A.Dummy<Guid>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<bool>>(result);
        Assert.False(result.IsSuccess);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }
    [Fact]
    public async Task StartSearchAsync_UpdateSuccess_ReturnsTrueBoolResult()
    {
        var settingsGuid = Guid.NewGuid();
        var userGuid = Guid.NewGuid();
        var dummySearchSettings = A.Dummy<SearchSettings>();
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(settingsGuid, userGuid))
            .Returns(Result<SearchSettings>.Success(dummySearchSettings));

        A.CallTo(() => _searchSettingsRepository.UpdateAsync(dummySearchSettings, userGuid))
            .Returns(Result<SearchSettings>.Success(dummySearchSettings));

        //Act
        var result = await _sut.StartSearchAsync(settingsGuid, userGuid);

        //Assert
        Assert.IsType<Result<bool>>(result);
        Assert.True(result.IsSuccess);
        Assert.True(dummySearchSettings.IsActive);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(dummySearchSettings, userGuid))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task StopSearchAsync_EntityNotFound_ReturnsFalseBoolResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Failure("Not found", 404));

        //Act
        var result = await _sut.StopSearchAsync(A.Dummy<Guid>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<bool>>(result);
        Assert.False(result.IsSuccess);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .MustNotHaveHappened();
    }
    [Fact]
    public async Task StopSearchAsync_UpdateUnsuccessful_ReturnsFalseBoolResult()
    {
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(A<Guid>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Success(A.Dummy<SearchSettings>()));

        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .Returns(Result<SearchSettings>.Failure("Could not update", 404));

        //Act
        var result = await _sut.StopSearchAsync(A.Dummy<Guid>(), A.Dummy<Guid>());

        //Assert
        Assert.IsType<Result<bool>>(result);
        Assert.False(result.IsSuccess);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(A<SearchSettings>._, A<Guid>._))
            .MustHaveHappenedOnceExactly();
    }
    [Fact]
    public async Task StopAsync_UpdateSuccess_ReturnsTrueBoolResult()
    {
        var settingsGuid = Guid.NewGuid();
        var userGuid = Guid.NewGuid();
        var dummySearchSettings = A.Dummy<SearchSettings>();
        //Arrange
        A.CallTo(() => _searchSettingsRepository.GetByIdAsync(settingsGuid, userGuid))
            .Returns(Result<SearchSettings>.Success(dummySearchSettings));

        A.CallTo(() => _searchSettingsRepository.UpdateAsync(dummySearchSettings, userGuid))
            .Returns(Result<SearchSettings>.Success(dummySearchSettings));

        //Act
        var result = await _sut.StopSearchAsync(settingsGuid, userGuid);

        //Assert
        Assert.IsType<Result<bool>>(result);
        Assert.True(result.IsSuccess);
        Assert.False(dummySearchSettings.IsActive);
        A.CallTo(() => _searchSettingsRepository.UpdateAsync(dummySearchSettings, userGuid))
            .MustHaveHappenedOnceExactly();
    }
}