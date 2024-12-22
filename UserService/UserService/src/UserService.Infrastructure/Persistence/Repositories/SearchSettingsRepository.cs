using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Persistence.Repositories;

public class SearchSettingsRepository(string collectionName, string connectionString, string databaseName) :
    MongoRepositoryBase<SearchSettings>(collectionName, connectionString, databaseName), ISearchSettingsRepository
{
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