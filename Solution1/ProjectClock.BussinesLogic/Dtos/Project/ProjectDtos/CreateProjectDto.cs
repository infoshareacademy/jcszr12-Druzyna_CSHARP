namespace ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos
{
    public class CreateProjectDto
    {
       public int UserId { get; set; }
       public string ProjectId { get; set; }
       public string ProjectName { get; set; }
       public string OrganizationId { get; set; }
       public string OrganizationName { get; set; }
    }
}
