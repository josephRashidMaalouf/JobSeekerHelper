using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UserService.Domain.Entities;

public class EntityWithUserIdBase : EntityBase
{
    [BsonRepresentation(BsonType.String)] 
    public required Guid UserId { get; set; }
}