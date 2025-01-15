using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JobSeekerHelper.Nuget.Entities;

public abstract class EntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; } = Guid.NewGuid();
}