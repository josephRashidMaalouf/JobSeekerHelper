namespace UserService.Domain.Models;

public class EducationItems
{
    public required string SchoolName { get; set; }
    public required string Program { get; set; }
    public required string Description { get; set; }
    public required string Degree { get; set; }
    public required bool IsOnGoing { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}