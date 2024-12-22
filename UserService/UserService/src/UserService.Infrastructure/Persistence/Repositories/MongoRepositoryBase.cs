using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Persistence.Repositories;

public abstract class MongoRepositoryBase<TEntity> : ICrud<TEntity> where TEntity : EntityBase
{
    protected readonly IMongoCollection<TEntity> _collection;
    
    protected MongoRepositoryBase(string collectionName, string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var db = client.GetDatabase(databaseName);
        _collection = db.GetCollection<TEntity>(collectionName);
    }
    
    public async Task<Result<TEntity>> GetByIdAsync(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);

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

    public async Task<Result<TEntity>> AddAsync(TEntity entity)
    {
        try
        {
            await _collection.InsertOneAsync(entity);
            return Result<TEntity>.Success(entity);
        }
        catch (MongoWriteException ex)
        {
            return Result<TEntity>.Failure(ex.Message, 401);
        }
        catch (Exception ex)
        {
            return Result<TEntity>.Failure(ex.Message, 500);
        }
    }

    public async Task<Result<TEntity>> UpdateAsync(TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
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

    public async Task<Result<Guid>> DeleteAsync(Guid id)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);

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