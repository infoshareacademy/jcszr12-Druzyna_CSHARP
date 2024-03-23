using ProjectClock.Database.Entities;

namespace ProjectClock.BusinessLogic.Dtos.OrganizationDto
{
    public class ManageOrganizationDto
    {
        public List<Database.Entities.Organization> Organizations { get; set; }
        public List<Database.Entities.User>? Users { get; set; } = new List<Database.Entities.User> ();
        public List<Database.Entities.Project>? Projects { get; set; } = new List<Database.Entities.Project>();
    }
}