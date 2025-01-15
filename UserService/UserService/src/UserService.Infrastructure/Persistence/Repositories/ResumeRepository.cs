using JobSeekerHelper.Nuget.Repositories;
using JobSeekerHelper.Nuget.Results;
using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Infrastructure.Persistence.Repositories;

public class ResumeRepository(string collectionName, string connectionString, string databaseName) :
    MongoRepositoryBase<Resume>(collectionName, connectionString, databaseName), IResumeRepository
{
    public async Task<Result<List<Resume>>> GetAllByUserIdAsync(Guid userId)
    {
        var filter = Builders<Resume>.Filter.Eq(x => x.UserId, userId);

        try
        {
            var result = await _collection.Find(filter).ToListAsync();

            if (!result.Any())
            {
                return Result<List<Resume>>
                    .Failure($"No resumes found for user with id {userId}", 404);
            }

            return Result<List<Resume>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<Resume>>.Failure(ex.Message, 500);
        }
    }
}