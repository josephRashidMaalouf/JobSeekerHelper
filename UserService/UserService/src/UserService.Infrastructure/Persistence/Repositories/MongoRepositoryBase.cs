using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Persistence.Repositories;

public abstract class MongoRepositoryBase<TEntity> : ICrud<TEntity> where TEntity : EntityWithUserIdBase
{
    protected readonly IMongoCollection<TEntity> _collection;

    protected MongoRepositoryBase(string collectionName, string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        _collection = db.GetCollection<TEntity>(collectionName);
    }

    public async Task<Result<TEntity>> GetByIdAsync(Guid id, Guid userId)
    {
        var filter = Builders<TEntity>.Filter.And(
            Builders<TEntity>.Filter.Eq(e => e.Id, id),
            Builders<TEntity>.Filter.Eq(e => e.UserId, userId)
        );

        try
        {
            var result = await _collection.Find(filter).FirstOrDefaultAsync();

            if (result is null)
            {
                return Result<TEntity>.Failure($"Entity with id {id} not found", 404);
            }

            return Result<TEntity>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<TEntity>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<TEntity>> AddAsync(TEntity entity, Guid userId)
    {
        if (entity.UserId != userId)
        {
            return Result<TEntity>.Failure($"Entity userId: {entity.UserId} is not equal to userId: {userId}", 400);
        }
        try
        {
            await _collection.InsertOneAsync(entity);
            return Result<TEntity>.Success(entity);
        }
        catch (MongoWriteException ex)
        {
            return Result<TEntity>.Failure(ex.Message, 400);
        }
        catch (Exception ex)
        {
            return Result<TEntity>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<TEntity>> UpdateAsync(TEntity entity, Guid userId)
    {
        var filter = Builders<TEntity>.Filter.And(
            Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id),
            Builders<TEntity>.Filter.Eq(e => e.UserId, userId)
        );

        var options = new ReplaceOptions() { IsUpsert = false };

        try
        {
            var updateResult = await _collection.ReplaceOneAsync(filter, entity, options);

            if (!updateResult.IsAcknowledged)
            {
                return Result<TEntity>.Failure($"Entity with id {entity.Id} not found and could thus not be updated",
                    404);
            }

            return Result<TEntity>.Success(entity);
        }
        catch (Exception ex)
        {
            return Result<TEntity>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<Guid>> DeleteAsync(Guid id, Guid userId)
    {
        var filter = Builders<TEntity>.Filter.And(
            Builders<TEntity>.Filter.Eq(e => e.Id, id),
            Builders<TEntity>.Filter.Eq(e => e.UserId, userId)
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