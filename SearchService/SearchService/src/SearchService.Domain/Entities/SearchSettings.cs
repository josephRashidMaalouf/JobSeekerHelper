using JobSeekerHelper.Nuget.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SearchService.Domain.Models;

namespace SearchService.Domain.Entities;

public class SearchSettings : EntityWithUserIdBase
{
    [BsonRepresentation(BsonType.String)]
    public required Guid ResumeId { get; set; }
    public required string SearchQuery { get; set; }
    public required Municipality Municipality { get; set; }
    public required SearchFrequency Frequency { get; set; }
    public required bool GenerateLetters { get; set; }
    public bool IsActive { get; set; }
}