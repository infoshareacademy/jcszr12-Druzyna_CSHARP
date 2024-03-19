using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Dtos.WorkingTime.WorkingTimeDtos;

public class StartWorkingTimeDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string? ProjectName { get; set; }
    public List<Database.Entities.Project> Projects { get; set; } = new List<Database.Entities.Project>();
}
