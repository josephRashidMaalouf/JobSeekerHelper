using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JobSeekerHelper.Nuget.Entities;

public abstract class EntityWithUserIdBase : EntityBase
{
    [BsonRepresentation(BsonType.String)]
    public required Guid UserId { get; set; }
}