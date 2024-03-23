namespace ProjectClock.BusinessLogic.Dtos.Project.ProjectDtos
{
    public class CreateProjectDto
    {
       public int UserId { get; set; }
       public List<Database.Entities.Organization> UserOrganizations {  get; set; }
       public string ProjectName { get; set; }
       public string OrganizationId { get; set; }
    }
}
