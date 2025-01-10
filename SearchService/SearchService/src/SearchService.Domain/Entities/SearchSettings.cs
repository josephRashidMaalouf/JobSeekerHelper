using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SearchService.Domain.Models;

namespace SearchService.Domain.Entities;

public class SearchSettings
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public required Guid Id { get; set; } = Guid.NewGuid();

    [BsonRepresentation(BsonType.String)] 
    public required Guid UserId { get; set; }

    public required string SearchQuery { get; set; }
    public required Municipality Municipality { get; set; }
    public required SearchFrequency Frequency { get; set; }
    public required Guid ResumeId { get; set; }
    public required bool GenerateLetters { get; set; }
    public bool IsActive { get; set; }
}