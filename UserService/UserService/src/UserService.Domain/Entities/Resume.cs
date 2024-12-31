using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UserService.Domain.Models;

namespace UserService.Domain.Entities;

public class Resume : EntityWithUserIdBase
{

    public required Email Email { get; set; }
    public required string Name { get; set; }
    public required string AboutMe { get; set; }
    public List<string> Skills { get; set; } = [];
    public List<string> Languages { get; set; } = [];
    public List<WorkItem> WorkItems { get; set; } = [];
    public List<EducationItems> EducationItems { get; set; } = [];
}