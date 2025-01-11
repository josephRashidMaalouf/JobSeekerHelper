using MongoDB.Driver;
using SearchService.Domain.Entities;
using SearchService.Domain.Interfaces;
using SearchService.Domain.Models;

namespace SearchService.Infrastructure.Repositories;

public class SearchSettingsRepository : ISearchSettingsRepository
{
    private readonly IMongoCollection<SearchSettings> _collection;

    public SearchSettingsRepository(string collectionName, string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        _collection = db.GetCollection<SearchSettings>(collectionName);
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

    public async Task<Result<SearchSettings>> GetByIdAsync(Guid id, Guid userId)
    {
        var filter = Builders<SearchSettings>.Filter.And(
            Builders<SearchSettings>.Filter.Eq(e => e.Id, id),
            Builders<SearchSettings>.Filter.Eq(e => e.UserId, userId)
        );

        try
        {
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            if (result is null)
            {
                return Result<SearchSettings>.Failure($"Entity with id {id} not found", 404);
            }

            return Result<SearchSettings>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<SearchSettings>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<SearchSettings>> AddAsync(SearchSettings entity, Guid userId)
    {
        if (entity.UserId != userId)
        {
            return Result<SearchSettings>.Failure($"Entity userId: {entity.UserId} is not equal to userId: {userId}", 400);
        }

        try
        {
            await _collection.InsertOneAsync(entity);
            return Result<SearchSettings>.Success(entity);
        }
        catch (MongoWriteException ex)
        {
            return Result<SearchSettings>.Failure(ex.Message, 400);
        }
        catch (Exception ex)
        {
            return Result<SearchSettings>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<SearchSettings>> UpdateAsync(SearchSettings entity, Guid userId)
    {
        var filter = Builders<SearchSettings>.Filter.And(
            Builders<SearchSettings>.Filter.Eq(e => e.Id, entity.Id),
            Builders<SearchSettings>.Filter.Eq(e => e.UserId, userId)
        );

        var options = new ReplaceOptions() { IsUpsert = false };

        try
        {
            var updateResult = await _collection.ReplaceOneAsync(filter, entity, options);

            if (!updateResult.IsAcknowledged)
            {
                return Result<SearchSettings>.Failure($"Entity with id {entity.Id} not found and could thus not be updated",
                    404);
            }

            return Result<SearchSettings>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<SearchSettings>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id, Guid userId)
    {
        var filter = Builders<SearchSettings>.Filter.And(
            Builders<SearchSettings>.Filter.Eq(e => e.Id, id),
            Builders<SearchSettings>.Filter.Eq(e => e.UserId, userId)
        );

        try
        {
            var result = await _collection.DeleteOneAsync(filter);

            if (!result.IsAcknowledged)
            {
                return Result<Guid>.Failure($"Entity with id {id} not found. No entity has been deleted", 404);
            }

            return Result<Guid>.Success(id);
        }
        catch (Exception ex)
        {
            return Result<Guid>.Failure(ex.Message, 500);
        }
    }
}