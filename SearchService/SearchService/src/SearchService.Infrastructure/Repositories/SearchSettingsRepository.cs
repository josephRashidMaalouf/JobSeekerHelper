using JobSeekerHelper.Nuget.Repositories;
using JobSeekerHelper.Nuget.Results;
using MongoDB.Driver;
using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;

namespace SearchService.Infrastructure.Repositories;

public class SearchSettingsRepository : MongoRepositoryBase<SearchSettings>, ISearchSettingsRepository
{
    public SearchSettingsRepository(string collectionName, string connectionString, string databaseName) : base(
        collectionName, connectionString, databaseName)
    {
        
    }

    public async Task<Result<List<SearchSettings>>> GetAllByUserIdAsync(Guid userId)
    {
        var filter = Builders<SearchSettings>.Filter.Eq(x => x.UserId, userId);

        try
        {
            var result = await _collection.Find(filter).ToListAsync();
            return Result<List<SearchSettings>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<SearchSettings>>.Failure(ex.Message, 500);
        }
    }
}