namespace UserService.Domain.Models;

public class WorkItem
{
    public required string CompanyName { get; set; }
    public required string JobTitle { get; set; }
    public required string Description { get; set; }
    public required bool IsOnGoing { get; set; }
    public required DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
}