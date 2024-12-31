using UserService.Domain.Models;

namespace UserService.Domain.Entities;

public class SearchSettings : EntityWithUserIdBase
{
    public required string SearchQuery { get; set; }
    public required Municipality Municipality { get; set; }
    public required SearchFrequency Frequency { get; set; }
    public required Guid ResumeId { get; set; }
    public required bool GenerateLetters { get; set; }
}